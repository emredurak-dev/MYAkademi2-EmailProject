using CoreEmailProject.Context;
using CoreEmailProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreEmailProject.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;

        public UserController(AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Profile()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.ProfilePicture = values.ProfileImageUrl;
            ViewBag.Name = values.Name;
            ViewBag.Surname = values.Surname;
            ViewBag.Email = values.Email;
            ViewBag.Username = values.UserName;
            ViewBag.RecipientEmailAddressCount = _appDbContext.EmailMessages
                                    .Where(x => x.RecipientEmailAddress == values.Email)
                                    .Count();

            ViewBag.SenderEmailAddressCount = _appDbContext.EmailMessages
                                                 .Where(x => x.SenderEmailAddress == values.Email)
                                                 .Count();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AppUser updatedUser, string profileImageUrl, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = updatedUser.Name;
            user.Surname = updatedUser.Surname;
            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;

            if (!string.IsNullOrEmpty(profileImageUrl))
            {
                user.ProfileImageUrl = profileImageUrl;
            }

            if (!string.IsNullOrEmpty(newPassword))
            {
                var hasPassword = await _userManager.HasPasswordAsync(user);
                if (hasPassword)
                {
                    var removeResult = await _userManager.RemovePasswordAsync(user);
                    if (!removeResult.Succeeded)
                    {
                        foreach (var error in removeResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View("Profile", user);
                    }
                }

                var addResult = await _userManager.AddPasswordAsync(user, newPassword);
                if (!addResult.Succeeded)
                {
                    foreach (var error in addResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View("Profile", user);
                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Profil başarıyla güncellendi!";
                return RedirectToAction("Login","Auth");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View("Profile", user);
            }
        }
    }
}