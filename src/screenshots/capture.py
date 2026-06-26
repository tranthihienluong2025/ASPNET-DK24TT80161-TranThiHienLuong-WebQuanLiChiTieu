"""Auto-login admin/user1 and capture screenshots of all main pages."""
import asyncio
import sys
from pathlib import Path
from playwright.async_api import async_playwright

sys.stdout.reconfigure(encoding="utf-8")

BASE_URL = "http://localhost:5000"
SCREENSHOT_DIR = Path(__file__).parent

ACCOUNTS = [
    {"email": "admin@qlct.com", "password": "12345678", "folder": "admin"},
    {"email": "user1@qlct.com", "password": "12345678", "folder": "user1"},
]

# Các trang public (chụp 1 lần, không cần login)
PUBLIC_PAGES = [
    ("01_home", "/"),
    ("02_privacy", "/Home/Privacy"),
    ("03_login", "/Account/Login"),
    ("04_register", "/Account/Register"),
    ("05_forgot_password", "/Account/ForgotPassword"),
]

# Các trang bên trong (yêu cầu đăng nhập), chụp riêng cho từng tài khoản
AUTH_PAGES = [
    ("01_dashboard", "/Dashboard"),
    ("02_chitieu_index", "/ChiTieu"),
    ("03_chitieu_create", "/ChiTieu/Create"),
    ("04_danhmuc", "/DanhMuc"),
    ("05_thongke", "/ThongKe"),
    ("06_lichchitieu_index", "/LichChiTieu"),
    ("07_lichchitieu_create", "/LichChiTieu/Create"),
    ("08_gioihan", "/GioiHanChiTieu"),
    ("09_thongbao", "/ThongBao"),
    ("10_profile", "/Profile"),
    ("11_settings", "/Settings"),
]


async def capture_page(page, url_path, output_path):
    print(f"  -> {url_path}")
    await page.goto(BASE_URL + url_path, wait_until="networkidle", timeout=60000)
    await page.wait_for_timeout(1500)
    await page.screenshot(path=str(output_path), full_page=True)


async def login(page, email, password):
    await page.goto(BASE_URL + "/Account/Login", wait_until="networkidle", timeout=60000)
    await page.fill('input[name="Email"]', email)
    await page.fill('input[name="Password"]', password)
    await page.click('button[type="submit"]')
    await page.wait_for_url(f"{BASE_URL}/Dashboard*", timeout=30000)


async def logout(page):
    # Form logout là POST, gọi qua JS để chắc chắn
    await page.evaluate("""
        () => {
            const form = document.querySelector('form[action*="Logout"]');
            if (form) form.submit();
        }
    """)
    await page.wait_for_timeout(1500)


async def main():
    SCREENSHOT_DIR.mkdir(exist_ok=True)
    public_dir = SCREENSHOT_DIR / "public"
    public_dir.mkdir(exist_ok=True)

    async with async_playwright() as p:
        browser = await p.chromium.launch(headless=True)
        context = await browser.new_context(
            viewport={"width": 1440, "height": 900},
            ignore_https_errors=True,
        )
        page = await context.new_page()

        # 1) Public pages
        print("[Public]")
        for name, path in PUBLIC_PAGES:
            await capture_page(page, path, public_dir / f"{name}.png")

        # 2) Authenticated pages cho từng tài khoản
        for acc in ACCOUNTS:
            print(f"\n[{acc['folder']}] Login as {acc['email']}")
            user_dir = SCREENSHOT_DIR / acc["folder"]
            user_dir.mkdir(exist_ok=True)

            # tạo context mới cho mỗi user để cookie sạch
            await context.clear_cookies()
            await login(page, acc["email"], acc["password"])

            for name, path in AUTH_PAGES:
                try:
                    await capture_page(page, path, user_dir / f"{name}.png")
                except Exception as e:
                    print(f"     ! Bỏ qua {path}: {e}")

            try:
                await logout(page)
            except Exception:
                pass

        await browser.close()
    print("\nXong! Ảnh nằm trong:", SCREENSHOT_DIR)


if __name__ == "__main__":
    asyncio.run(main())
