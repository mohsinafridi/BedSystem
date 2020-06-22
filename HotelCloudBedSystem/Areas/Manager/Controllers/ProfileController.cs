using HotelCloudBedSystem.Areas.Manager.ViewModels;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class ProfileController : Controller
    {
        private UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        #region ProfilePicture
        [HttpGet]
        public IActionResult _UpdateProfilePicture()
        {

            ManagerProfileViewModel model = new ManagerProfileViewModel();
            var users = _userManager.GetUsersInRoleAsync("Admin").Result;
            if (User != null)
            {
                foreach (var user in users)
                {
                    var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;

                    if (user == OnlineUser)
                    {
                        model.AvatarImage = user.AvatarImage;
                    }
                }
            }
            return PartialView(model);
        }

        [HttpPost]

        public IActionResult _UpdateProfilePicture(ManagerProfileViewModel model)
        {

            if (ModelState.IsValid)
            {
                var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;
                if (OnlineUser != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        model.avatarimage.CopyToAsync(memoryStream);
                        OnlineUser.AvatarImage = memoryStream.ToArray();

                    }

                    var result = _userManager.UpdateAsync(OnlineUser).Result;

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", new { area = "Manager", controller = "ManagerProfile" });
                    }
                }

            }
            return PartialView();
        }

        #endregion
    }
}
