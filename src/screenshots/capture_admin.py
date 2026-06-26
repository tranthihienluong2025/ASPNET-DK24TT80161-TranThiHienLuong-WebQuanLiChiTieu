"""Capture admin-only pages: user list, create, edit."""
import asyncio
import sys
from pathlib import Path
from playwright.async_api import async_playwright

sys.stdout.reconfigure(encoding="utf-8")

BASE_URL = "http://localhost:5000"
OUT_DIR = Path(__file__).parent / "admin"
OUT_DIR.mkdir(exist_ok=True)


async def login(page, email, password):
    await page.goto(BASE_URL + "/Account/Login", wait_until="networkidle", timeout=60000)
    await page.fill('input[name="Email"]', email)
    await page.fill('input[name="Password"]', password)
    await page.click('button[type="submit"]')
    await page.wait_for_url(f"{BASE_URL}/Dashboard*", timeout=30000)


async def capture(page, path, file_name):
    print(f"  -> {path}")
    await page.goto(BASE_URL + path, wait_until="networkidle", timeout=60000)
    await page.wait_for_timeout(1500)
    await page.screenshot(path=str(OUT_DIR / file_name), full_page=True)


async def main():
    async with async_playwright() as p:
        browser = await p.chromium.launch(headless=True)
        context = await browser.new_context(
            viewport={"width": 1440, "height": 900},
            ignore_https_errors=True,
        )
        page = await context.new_page()

        print("[admin] Login")
        await login(page, "admin@qlct.com", "12345678")

        # Lấy ID của user1 để vào trang Edit
        await capture(page, "/Admin/Users", "12_admin_users.png")
        await capture(page, "/Admin/Create", "13_admin_create.png")
        await capture(page, "/Admin/Edit/2", "14_admin_edit.png")

        await browser.close()
    print("\nDone:", OUT_DIR)


if __name__ == "__main__":
    asyncio.run(main())
