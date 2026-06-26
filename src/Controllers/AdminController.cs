using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyChiTieu.Data;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyChiTieu.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        private int CurrentUserId()
            => int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

        public async Task<IActionResult> Users(string? q)
        {
            var query = _context.NguoiDungs.AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                var keyword = q.Trim().ToLower();
                query = query.Where(u =>
                    u.Email.ToLower().Contains(keyword) ||
                    (u.HoTen != null && u.HoTen.ToLower().Contains(keyword)));
            }

            var users = await query
                .OrderByDescending(u => u.LoaiTaiKhoan)
                .ThenByDescending(u => u.NgayDangKy)
                .Select(u => new
                {
                    User = u,
                    SoChiTieu = _context.ChiTieus.Count(c => c.NguoiDungId == u.Id),
                    TongChiTieu = _context.ChiTieus.Where(c => c.NguoiDungId == u.Id).Sum(c => (decimal?)c.SoTien) ?? 0m
                })
                .ToListAsync();

            ViewBag.Keyword = q;
            ViewBag.TongUser = await _context.NguoiDungs.CountAsync();
            ViewBag.TongAdmin = await _context.NguoiDungs.CountAsync(u => u.LoaiTaiKhoan == 1);
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AdminUserViewModel { LoaiTaiKhoan = 0 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminUserViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.MatKhau))
            {
                ModelState.AddModelError(nameof(model.MatKhau), "Vui lòng nhập mật khẩu.");
            }
            if (await _context.NguoiDungs.AnyAsync(u => u.Email == model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "Email đã tồn tại.");
            }
            if (!ModelState.IsValid) return View(model);

            var user = new NguoiDung
            {
                Email = model.Email,
                HoTen = model.HoTen,
                MatKhau = HashPassword(model.MatKhau!),
                LoaiTaiKhoan = model.LoaiTaiKhoan,
                NgayDangKy = DateTime.Now,
                NhanEmailNhacNho = false,
                AvatarUrl = $"https://ui-avatars.com/api/?name={Uri.EscapeDataString(model.HoTen ?? model.Email)}&background=4BC0C0&color=fff"
            };
            _context.NguoiDungs.Add(user);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Đã tạo tài khoản {user.Email}.";
            return RedirectToAction(nameof(Users));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.NguoiDungs.FindAsync(id);
            if (user == null) return NotFound();

            var vm = new AdminUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                HoTen = user.HoTen,
                LoaiTaiKhoan = user.LoaiTaiKhoan ?? 0
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdminUserViewModel model)
        {
            if (id != model.Id) return BadRequest();
            var user = await _context.NguoiDungs.FindAsync(id);
            if (user == null) return NotFound();

            if (await _context.NguoiDungs.AnyAsync(u => u.Email == model.Email && u.Id != id))
            {
                ModelState.AddModelError(nameof(model.Email), "Email đã được sử dụng bởi tài khoản khác.");
            }
            if (!ModelState.IsValid) return View(model);

            var isSelf = user.Id == CurrentUserId();
            user.Email = model.Email;
            user.HoTen = model.HoTen;
            if (!isSelf)
            {
                user.LoaiTaiKhoan = model.LoaiTaiKhoan;
            }
            if (!string.IsNullOrWhiteSpace(model.MatKhau))
            {
                user.MatKhau = HashPassword(model.MatKhau);
            }
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Đã cập nhật tài khoản {user.Email}.";
            return RedirectToAction(nameof(Users));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleRole(int id)
        {
            var user = await _context.NguoiDungs.FindAsync(id);
            if (user == null) return NotFound();

            if (user.Id == CurrentUserId())
            {
                TempData["Error"] = "Không thể thay đổi vai trò của chính bạn.";
                return RedirectToAction(nameof(Users));
            }

            user.LoaiTaiKhoan = user.LoaiTaiKhoan == 1 ? 0 : 1;
            await _context.SaveChangesAsync();
            TempData["Success"] = $"Đã cập nhật vai trò cho {user.Email}.";
            return RedirectToAction(nameof(Users));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.NguoiDungs.FindAsync(id);
            if (user == null) return NotFound();

            if (user.Id == CurrentUserId())
            {
                TempData["Error"] = "Không thể xóa tài khoản đang đăng nhập.";
                return RedirectToAction(nameof(Users));
            }

            _context.ChiTieus.RemoveRange(_context.ChiTieus.Where(c => c.NguoiDungId == id));
            _context.ChiTieuTheoLiches.RemoveRange(_context.ChiTieuTheoLiches.Where(c => c.NguoiDungId == id));
            _context.GioiHanChiTieus.RemoveRange(_context.GioiHanChiTieus.Where(c => c.NguoiDungId == id));
            _context.ThongBaos.RemoveRange(_context.ThongBaos.Where(t => t.NguoiDungId == id));
            _context.LichSuNhanNhacs.RemoveRange(_context.LichSuNhanNhacs.Where(l => l.NguoiDungId == id));
            _context.NguoiDungs.Remove(user);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Đã xóa tài khoản {user.Email}.";
            return RedirectToAction(nameof(Users));
        }
    }
}
