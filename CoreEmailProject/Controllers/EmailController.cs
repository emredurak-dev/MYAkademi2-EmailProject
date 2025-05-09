using CoreEmailProject.Context;
using CoreEmailProject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreEmailProject.Controllers
{
    public class EmailController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;

        public EmailController(AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Inbox(string searchTerm)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userEmail = user.Email;
            ViewBag.email = userEmail;
            ViewBag.namesurname = user.Name + " " + user.Surname;
            ViewBag.SearchTerm = searchTerm;
            var messages = _appDbContext.EmailMessages
                .Where(x => x.RecipientEmailAddress == userEmail && x.IsRead == false && x.IsDeleted == false);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                messages = messages.Where(x => x.EmailSubject.ToLower().Contains(searchTerm.ToLower()));
            }
            return View(messages.ToList());
        }

        public async Task<IActionResult> Sent(string searchTerm)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string userEmail = user.Email;
            ViewBag.email = userEmail;
            ViewBag.namesurname = user.Name + " " + user.Surname;
            ViewBag.SearchTerm = searchTerm;
            var sentEmails = _appDbContext.EmailMessages
                .Where(x => x.SenderEmailAddress == userEmail && x.IsRead == false && x.IsDeleted == false);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                string term = searchTerm.ToLower();
                sentEmails = sentEmails.Where(x =>
                    x.EmailSubject.ToLower().Contains(term) ||
                    x.RecipientEmailAddress.ToLower().Contains(term) ||
                    x.EmailBody.ToLower().Contains(term)
                );
            }
            return View(sentEmails.ToList());
        }

        [HttpGet]
        public IActionResult Compose()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Compose(EmailMessage emailMessage)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            string senderEmail = values.Email;
            emailMessage.SenderEmailAddress = senderEmail;
            emailMessage.IsRead = false;
            emailMessage.SentAt = DateTime.Now;
            _appDbContext.Add(emailMessage);
            _appDbContext.SaveChanges();
            ViewBag.Success = "📨 Mesaj Uçtu!";
            return View("~/Views/Email/Compose.cshtml");
        }

        public IActionResult Read(int id)
        {
            var values = _appDbContext.EmailMessages.FirstOrDefault(x => x.Id == id);
            return View(values);
        }
        public async Task<IActionResult> ReadList()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userEmail = user.Email;
            ViewBag.email = userEmail;
            ViewBag.namesurname = user.Name + " " + user.Surname;
            var messages = _appDbContext.EmailMessages
                .Where(x =>
                    x.IsRead == true &&
                    x.IsDeleted == false &&
                    (
                        (x.RecipientEmailAddress == userEmail || x.SenderEmailAddress == userEmail)
                    )
                )
                .ToList();
            return View(messages);
        }


        [HttpPost]
        public async Task<IActionResult> BulkAction(List<int> Id, string actionType, string sourcePage)
        {
            if (Id == null || !Id.Any())
                return RedirectToAction(sourcePage);
            foreach (var id in Id)
            {
                var email = await _appDbContext.EmailMessages.FindAsync(id);
                if (email == null) continue;
                switch (actionType)
                {
                    case "read":
                        email.IsRead = true;
                        break;
                    case "unread":
                        email.IsRead = false;
                        break;
                    case "delete":
                        email.IsDeleted = true;
                        break;
                }
            }
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(sourcePage);
        }

        public IActionResult Trash()
        {
            var deletedMessage = _appDbContext.EmailMessages.Where(x => x.IsDeleted == true).ToList();
            return View(deletedMessage);
        }
    }
}