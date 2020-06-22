using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelCloudBedSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersAccountStatusController: Controller
    {

        private UserManager<AppUser> _userManager;

        public UsersAccountStatusController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult ManagerAccountStatus(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;

            if (user == null)
            {
                return NotFound();
            }
            if (user.IsEnable == true)
            {
                user.IsEnable = false;
            }
            else
            {
                user.IsEnable = true;
            }
            var result = _userManager.UpdateAsync(user).Result;

            if (result.Succeeded)
            {
                return RedirectToAction("Manager", new { area = "Admin", controller = "UserList" });

            }

            return View(user);
        }


        public IActionResult UserAccountStatus(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;

            if (user == null)
            {
                return NotFound();
            }
            if (user.IsEnable == true)
            {
                user.IsEnable = false;
            }
            else
            {
                user.IsEnable = true;
            }
            var result = _userManager.UpdateAsync(user).Result;

            if (result.Succeeded)
            {
                return RedirectToAction("AppLicationUser", new { area = "Admin", controller = "UserList" });

            }
            return View();
        }
    }
}
