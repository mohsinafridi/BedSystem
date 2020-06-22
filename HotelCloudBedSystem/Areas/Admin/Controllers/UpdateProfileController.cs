using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace HotelCloudBedSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class UpdateProfileController:Controller
    {
        private UserManager<AppUser> _userManager;

        public UpdateProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        #region ProfilePicture
        [HttpGet]
        public IActionResult _UpdateProfilePicture() {

            AdminProfilePictureUpdateViewModel model = new AdminProfilePictureUpdateViewModel();
            var users = _userManager.GetUsersInRoleAsync("Admin").Result;
            if(User != null)
            {
               foreach(var user in users)
                {
                    var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;

                    if(user == OnlineUser)
                    {
                        model.AvatarImages = user.AvatarImage;
                    }
                }
            }
            return PartialView(model);
        }

        [HttpPost]

        public IActionResult _UpdateProfilePicture(AdminProfilePictureUpdateViewModel model)
        {

            if (ModelState.IsValid)
            {
                var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;
                if(OnlineUser != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                          model.avatarImages.CopyToAsync(memoryStream);
                        OnlineUser.AvatarImage = memoryStream.ToArray();

                    }

                   var result= _userManager.UpdateAsync(OnlineUser).Result;

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", new { area = "Admin", controller = "AdminProfile" });
                    }
                }

            }
                return PartialView();
        }
            #endregion
        }
}
