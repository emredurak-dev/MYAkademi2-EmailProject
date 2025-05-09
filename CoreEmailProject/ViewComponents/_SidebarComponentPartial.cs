using CoreEmailProject.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CoreEmailProject.Entities;

namespace CoreEmailProject.ViewComponents
{
    public class _SidebarComponentPartial : ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public _SidebarComponentPartial(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userName = HttpContext.User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                // Kullanıcı giriş yapmamışsa sayıyı 0 döndür
                ViewBag.RecipientEmailAddressCount = 0;
                ViewBag.SenderEmailAddressCount = 0;
                ViewBag.ReadCount = 0;
                ViewBag.TrashCount = 0;
                return View();
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return View(); // Hata varsa boş view
            }

            var email = user.Email;

            ViewBag.RecipientEmailAddressCount = _context.EmailMessages
                .Where(x => x.RecipientEmailAddress == email && !x.IsRead && !x.IsDeleted)
                .Count();

            ViewBag.SenderEmailAddressCount = _context.EmailMessages
                .Where(x => x.SenderEmailAddress == email && !x.IsRead && !x.IsDeleted)
                .Count();

            ViewBag.ReadCount = _context.EmailMessages
                .Where(x => (x.SenderEmailAddress == email || x.RecipientEmailAddress == email) && x.IsRead && !x.IsDeleted)
                .Count();

            ViewBag.TrashCount = _context.EmailMessages
                .Where(x => (x.SenderEmailAddress == email || x.RecipientEmailAddress == email) && x.IsDeleted)
                .Count();

            return View();
        }
    }
}
