using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyChiTieu.Data;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuanLyChiTieu.ViewComponents
{
    public class AvatarViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public AvatarViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string avatarUrl = "/images/default-avatar.png"; // Đường dẫn mặc định
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(((ClaimsPrincipal)User).FindFirstValue(ClaimTypes.NameIdentifier));
                var user = await _context.NguoiDungs.FindAsync(userId);
                if (user != null && !string.IsNullOrEmpty(user.AvatarUrl))
                {
                    avatarUrl = user.AvatarUrl;
                }
            }
            return View("Default", avatarUrl);
        }
    }
}