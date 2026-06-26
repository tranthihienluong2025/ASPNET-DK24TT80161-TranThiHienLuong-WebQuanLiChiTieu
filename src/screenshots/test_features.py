"""End-to-end feature tests using Playwright. Uses direct POST via browser cookies for reliability."""
import asyncio
import sys
import re
from datetime import datetime
from playwright.async_api import async_playwright

sys.stdout.reconfigure(encoding="utf-8")

BASE_URL = "http://localhost:5000"
ADMIN = {"email": "admin@qlct.com", "password": "12345678"}
USER = {"email": "user1@qlct.com", "password": "12345678"}

results = []


def log(name, ok, detail=""):
    status = "PASS" if ok else "FAIL"
    line = f"[{status}] {name}" + (f" -- {detail}" if detail else "")
    print(line)
    results.append((name, ok, detail))


async def login(page, email, password):
    await page.goto(f"{BASE_URL}/Account/Login", wait_until="networkidle")
    await page.fill('input[name="Email"]', email)
    await page.fill('input[name="Password"]', password)
    await page.click('button[type="submit"]')
    await page.wait_for_url(re.compile(r".*/Dashboard.*"), timeout=15000)


async def logout(page):
    await page.evaluate("""() => {
        const f = document.querySelector('form[action*="Logout"]');
        if (f) f.submit();
    }""")
    await page.wait_for_timeout(1500)


async def get_token(page, url):
    """Get antiforgery token from a page that has a form."""
    await page.goto(f"{BASE_URL}{url}", wait_until="networkidle")
    return await page.evaluate(
        "() => document.querySelector('input[name=\"__RequestVerificationToken\"]').value"
    )


async def row_contains(page, text):
    """Check whether a tbody row truly contains the text (avoid false positives from echoed search input)."""
    return await page.evaluate(
        f"""() => {{
            const rows = document.querySelectorAll('tbody tr');
            for (const r of rows) {{
                if (r.textContent.includes({text!r})) return true;
            }}
            return false;
        }}"""
    )


async def row_contains(page, text):
    """True if any tbody row contains text (avoids matching the search input echo)."""
    return await page.evaluate(
        f"""() => {{
            const rows = document.querySelectorAll('tbody tr');
            for (const r of rows) {{
                if (r.textContent.includes({text!r})) return true;
            }}
            return false;
        }}"""
    )


# ---------- Tests ----------

async def test_login_wrong_password(page):
    try:
        await page.goto(f"{BASE_URL}/Account/Login")
        await page.fill('input[name="Email"]', USER["email"])
        await page.fill('input[name="Password"]', "wrongpass")
        await page.click('button[type="submit"]')
        await page.wait_for_timeout(1500)
        body = await page.content()
        ok = "không đúng" in body or "Login" in page.url
        log("Login sai mật khẩu báo lỗi", ok)
    except Exception as e:
        log("Login sai mật khẩu", False, str(e))


async def test_login_logout(page):
    try:
        await login(page, USER["email"], USER["password"])
        log("Login user1", "/Dashboard" in page.url)
        await logout(page)
        log("Logout", "/Login" in page.url or "/Home" in page.url or page.url.endswith("/"))
    except Exception as e:
        log("Login/Logout", False, str(e))


async def test_pages_load(page, paths, label):
    for p in paths:
        try:
            resp = await page.goto(f"{BASE_URL}{p}", wait_until="networkidle", timeout=20000)
            ok = resp and resp.status == 200
            log(f"{label} GET {p}", ok, f"status={resp.status if resp else 'none'}")
        except Exception as e:
            log(f"{label} GET {p}", False, str(e))


async def test_danhmuc_crud(page):
    name = f"Test_DM_{datetime.now().strftime('%H%M%S')}"
    try:
        token = await get_token(page, "/DanhMuc")
        resp = await page.request.post(
            f"{BASE_URL}/DanhMuc/CreateOrEdit",
            form={
                "__RequestVerificationToken": token,
                "TenDanhMuc": name,
                "MauSac": "#123456",
                "TuDongPhanLoai": "true",
                "Id": "0",
            },
        )
        await page.goto(f"{BASE_URL}/DanhMuc", wait_until="networkidle")
        body = await page.content()
        log("DanhMuc - Create", name in body, f"status={resp.status}")

        # Find ID
        created_id = await page.evaluate(
            f"""() => {{
                const forms = document.querySelectorAll('form[action*="/DanhMuc/Delete/"]');
                for (const f of forms) {{
                    const tr = f.closest('tr') || f.closest('.card');
                    if (tr && tr.textContent.includes({name!r})) {{
                        const m = f.action.match(/Delete\\/(\\d+)/);
                        if (m) return m[1];
                    }}
                }}
                return null;
            }}"""
        )
        if created_id:
            resp = await page.request.post(
                f"{BASE_URL}/DanhMuc/Delete/{created_id}",
                form={"__RequestVerificationToken": token},
            )
            await page.goto(f"{BASE_URL}/DanhMuc", wait_until="networkidle")
            body = await page.content()
            log("DanhMuc - Delete", name not in body, f"status={resp.status}")
        else:
            log("DanhMuc - Delete", False, "id not found")
    except Exception as e:
        log("DanhMuc CRUD", False, str(e))


async def test_chitieu_crud(page):
    name = f"Test_CT_{datetime.now().strftime('%H%M%S')}"
    try:
        # Get token + first DanhMucId from Create page
        await page.goto(f"{BASE_URL}/ChiTieu/Create", wait_until="networkidle")
        token = await page.evaluate(
            "() => document.querySelector('input[name=\"__RequestVerificationToken\"]').value"
        )
        first_cat = await page.evaluate(
            "() => { const o = document.querySelector('select[name=\"DanhMucId\"] option[value]:not([value=\"\"])'); return o ? o.value : '1'; }"
        )

        # Create
        resp = await page.request.post(
            f"{BASE_URL}/ChiTieu/Create",
            form={
                "__RequestVerificationToken": token,
                "TenChiTieu": name,
                "SoTien": "55000",
                "NgayChi": "2026-06-14",
                "GhiChu": "Auto test",
                "DanhMucId": first_cat,
            },
        )
        await page.goto(f"{BASE_URL}/ChiTieu?searchString={name}", wait_until="networkidle")
        body = await page.content()
        log("ChiTieu - Create", name in body, f"status={resp.status}")
        log("ChiTieu - Search", name in body)

        # Find ID
        ct_id = await page.evaluate(
            f"""() => {{
                const links = document.querySelectorAll('a[href*="/ChiTieu/Edit/"], a[href*="/ChiTieu/Delete/"]');
                for (const a of links) {{
                    const tr = a.closest('tr') || a.closest('.card');
                    if (tr && tr.textContent.includes({name!r})) {{
                        const m = a.href.match(/(?:Edit|Delete)\\/(\\d+)/);
                        if (m) return m[1];
                    }}
                }}
                return null;
            }}"""
        )

        if ct_id:
            # Edit
            resp = await page.request.post(
                f"{BASE_URL}/ChiTieu/Edit/{ct_id}",
                form={
                    "__RequestVerificationToken": token,
                    "Id": ct_id,
                    "TenChiTieu": name + "_edited",
                    "SoTien": "66000",
                    "NgayChi": "2026-06-14",
                    "GhiChu": "Edited",
                    "DanhMucId": first_cat,
                },
            )
            await page.goto(f"{BASE_URL}/ChiTieu?searchString={name}_edited", wait_until="networkidle")
            body = await page.content()
            log("ChiTieu - Edit", (name + "_edited") in body, f"status={resp.status}")

            # Delete
            resp = await page.request.post(
                f"{BASE_URL}/ChiTieu/Delete/{ct_id}",
                form={"__RequestVerificationToken": token},
            )
            await page.goto(f"{BASE_URL}/ChiTieu?searchString={name}_edited", wait_until="networkidle")
            still_there = await row_contains(page, name + "_edited")
            log("ChiTieu - Delete", not still_there, f"status={resp.status}")
        else:
            log("ChiTieu - Edit", False, "id not found")
            log("ChiTieu - Delete", False, "id not found")
    except Exception as e:
        log("ChiTieu CRUD", False, str(e))


async def test_lich_chitieu(page):
    name = f"Test_LCT_{datetime.now().strftime('%H%M%S')}"
    try:
        await page.goto(f"{BASE_URL}/LichChiTieu/Create", wait_until="networkidle")
        token = await page.evaluate(
            "() => document.querySelector('input[name=\"__RequestVerificationToken\"]').value"
        )
        first_cat = await page.evaluate(
            "() => { const o = document.querySelector('select[name=\"DanhMucId\"] option[value]:not([value=\"\"])'); return o ? o.value : '1'; }"
        )

        resp = await page.request.post(
            f"{BASE_URL}/LichChiTieu/Create",
            form={
                "__RequestVerificationToken": token,
                "TenChiTieu": name,
                "SoTien": "100000",
                "NgayThucHien": "2026-06-25",
                "GhiChu": "Scheduled",
                "DanhMucId": first_cat,
            },
        )
        await page.goto(f"{BASE_URL}/LichChiTieu", wait_until="networkidle")
        body = await page.content()
        log("LichChiTieu - Create", name in body, f"status={resp.status}")

        # Delete
        lct_id = await page.evaluate(
            f"""() => {{
                const forms = document.querySelectorAll('form[action*="/LichChiTieu/Delete/"]');
                for (const f of forms) {{
                    const tr = f.closest('tr') || f.closest('.card');
                    if (tr && tr.textContent.includes({name!r})) {{
                        const m = f.action.match(/Delete\\/(\\d+)/);
                        if (m) return m[1];
                    }}
                }}
                return null;
            }}"""
        )
        if lct_id:
            resp = await page.request.post(
                f"{BASE_URL}/LichChiTieu/Delete/{lct_id}",
                form={"__RequestVerificationToken": token},
            )
            log("LichChiTieu - Delete", resp.status in (200, 302), f"status={resp.status}")
        else:
            log("LichChiTieu - Delete", False, "id not found")
    except Exception as e:
        log("LichChiTieu", False, str(e))


async def test_gioihan(page):
    try:
        token = await get_token(page, "/GioiHanChiTieu")
        # Set limit (POST to Set action)
        resp = await page.request.post(
            f"{BASE_URL}/GioiHanChiTieu/Set",
            form={
                "__RequestVerificationToken": token,
                "Thang": "12",
                "Nam": "2026",
                "SoTienToiDa": "9999000",
            },
        )
        await page.goto(f"{BASE_URL}/GioiHanChiTieu", wait_until="networkidle")
        body = await page.content()
        ok = "9.999.000" in body or "9,999,000" in body
        log("GioiHan - Set limit", ok, f"status={resp.status}")

        if ok:
            gh_id = await page.evaluate(
                """() => {
                    const forms = document.querySelectorAll('form[action*="/GioiHanChiTieu/Delete/"]');
                    for (const f of forms) {
                        const tr = f.closest('tr');
                        if (tr && tr.textContent.includes('Tháng 12') && tr.textContent.includes('2026')) {
                            const m = f.action.match(/Delete\\/(\\d+)/);
                            if (m) return m[1];
                        }
                    }
                    return null;
                }"""
            )
            if gh_id:
                resp = await page.request.post(
                    f"{BASE_URL}/GioiHanChiTieu/Delete/{gh_id}",
                    form={"__RequestVerificationToken": token},
                )
                log("GioiHan - Delete", resp.status in (200, 302), f"status={resp.status}")
            else:
                log("GioiHan - Delete", False, "id not found")
    except Exception as e:
        log("GioiHan", False, str(e))


async def test_profile_update(page):
    try:
        token = await get_token(page, "/Profile")
        new_name = f"User1_{datetime.now().strftime('%H%M%S')}"
        # Use multipart to satisfy form enctype but only HoTen field is needed
        resp = await page.request.post(
            f"{BASE_URL}/Profile/UpdateProfile",
            multipart={
                "__RequestVerificationToken": token,
                "HoTen": new_name,
                "Email": USER["email"],
            },
        )
        await page.goto(f"{BASE_URL}/Profile", wait_until="networkidle")
        body = await page.content()
        log("Profile - Update HoTen", new_name in body, f"status={resp.status}")
    except Exception as e:
        log("Profile", False, str(e))


async def test_settings(page):
    try:
        token = await get_token(page, "/Settings")
        resp = await page.request.post(
            f"{BASE_URL}/Settings",
            form={
                "__RequestVerificationToken": token,
                "NhanEmailNhacNho": "true",
                "TanSuatNhanNhac": "HangNgay",
                "GioNhanNhac": "08:30",
            },
        )
        ok = resp.status in (200, 302)
        log("Settings - Save", ok, f"status={resp.status}")
    except Exception as e:
        log("Settings", False, str(e))


async def test_admin_blocked_for_user(page):
    try:
        await page.goto(f"{BASE_URL}/Admin/Users", wait_until="networkidle")
        url = page.url
        ok = "AccessDenied" in url or "Login" in url
        log("Admin - Block non-admin access", ok, f"url={url}")
    except Exception as e:
        log("Admin - Block non-admin access", False, str(e))


async def test_admin_crud(page):
    new_email = f"test_{datetime.now().strftime('%H%M%S')}@qlct.com"
    try:
        token = await get_token(page, "/Admin/Create")

        # Create user
        resp = await page.request.post(
            f"{BASE_URL}/Admin/Create",
            form={
                "__RequestVerificationToken": token,
                "Email": new_email,
                "HoTen": "Auto Test User",
                "MatKhau": "12345678",
                "XacNhanMatKhau": "12345678",
                "LoaiTaiKhoan": "0",
            },
        )
        await page.goto(f"{BASE_URL}/Admin/Users?q={new_email}", wait_until="networkidle")
        body = await page.content()
        log("Admin - Create user", new_email in body, f"status={resp.status}")

        new_id = await page.evaluate(
            f"""() => {{
                const rows = document.querySelectorAll('tr');
                for (const r of rows) {{
                    if (r.textContent.includes({new_email!r})) {{
                        const link = r.querySelector('a[href*="/Admin/Edit/"]');
                        if (link) {{
                            const m = link.href.match(/Edit\\/(\\d+)/);
                            if (m) return m[1];
                        }}
                    }}
                }}
                return null;
            }}"""
        )

        if new_id:
            # Edit
            resp = await page.request.post(
                f"{BASE_URL}/Admin/Edit/{new_id}",
                form={
                    "__RequestVerificationToken": token,
                    "Id": new_id,
                    "Email": new_email,
                    "HoTen": "Auto Test Edited",
                    "LoaiTaiKhoan": "0",
                },
            )
            await page.goto(f"{BASE_URL}/Admin/Users?q={new_email}", wait_until="networkidle")
            body = await page.content()
            log("Admin - Edit user", "Auto Test Edited" in body, f"status={resp.status}")

            # Toggle role
            resp = await page.request.post(
                f"{BASE_URL}/Admin/ToggleRole/{new_id}",
                form={"__RequestVerificationToken": token},
            )
            log("Admin - Toggle role", resp.status in (200, 302), f"status={resp.status}")

            # Delete
            resp = await page.request.post(
                f"{BASE_URL}/Admin/Delete/{new_id}",
                form={"__RequestVerificationToken": token},
            )
            await page.goto(f"{BASE_URL}/Admin/Users?q={new_email}", wait_until="networkidle")
            still_there = await row_contains(page, new_email)
            log("Admin - Delete user", not still_there, f"status={resp.status}")
        else:
            log("Admin - Edit user", False, "id not found")
            log("Admin - Toggle role", False, "id not found")
            log("Admin - Delete user", False, "id not found")
    except Exception as e:
        log("Admin CRUD", False, str(e))


async def main():
    public_paths = ["/", "/Home/Privacy", "/Account/Login", "/Account/Register", "/Account/ForgotPassword"]
    auth_paths = ["/Dashboard", "/ChiTieu", "/DanhMuc", "/ThongKe", "/LichChiTieu", "/GioiHanChiTieu", "/ThongBao", "/Profile", "/Settings"]

    async with async_playwright() as p:
        browser = await p.chromium.launch(headless=True)
        context = await browser.new_context(viewport={"width": 1440, "height": 900})
        page = await context.new_page()

        print("=" * 60)
        print("PUBLIC PAGES")
        print("=" * 60)
        await test_pages_load(page, public_paths, "Public")

        print("\n" + "=" * 60)
        print("AUTH FLOW")
        print("=" * 60)
        await test_login_wrong_password(page)
        await test_login_logout(page)

        print("\n" + "=" * 60)
        print("USER1 - FEATURES")
        print("=" * 60)
        await login(page, USER["email"], USER["password"])
        await test_pages_load(page, auth_paths, "User1")
        await test_danhmuc_crud(page)
        await test_chitieu_crud(page)
        await test_lich_chitieu(page)
        await test_gioihan(page)
        await test_profile_update(page)
        await test_settings(page)
        await test_admin_blocked_for_user(page)
        await logout(page)

        print("\n" + "=" * 60)
        print("ADMIN - FEATURES")
        print("=" * 60)
        await context.clear_cookies()
        await login(page, ADMIN["email"], ADMIN["password"])
        await test_pages_load(page, auth_paths + ["/Admin/Users", "/Admin/Create"], "Admin")
        await test_admin_crud(page)
        await logout(page)

        await browser.close()

    total = len(results)
    passed = sum(1 for _, ok, _ in results if ok)
    print("\n" + "=" * 60)
    print(f"TOTAL: {passed}/{total} passed")
    print("=" * 60)
    if passed < total:
        print("\nFAILED:")
        for name, ok, detail in results:
            if not ok:
                print(f"  - {name}: {detail}")


if __name__ == "__main__":
    asyncio.run(main())
