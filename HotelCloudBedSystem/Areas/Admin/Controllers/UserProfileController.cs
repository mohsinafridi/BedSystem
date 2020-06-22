using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelCloudBedSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserProfileController : Controller
    {
        private UserManager<AppUser> _userManager;

        public UserProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(string id)
        {
           
            var model = new UserProfileViewModel();
            var user = _userManager.FindByIdAsync(id).Result;
            //var findRoles = _userManager.GetRolesAsync(user).Result;
            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.AvatarImage = user.AvatarImage;
                model.PhoneNo = user.PhoneNumber;
                model.EmailConfirmed = user.EmailConfirmed;
                model.IsEnabled = user.IsEnable;
                //model.role = user.AppRole.Name;

            }
            return View(model);

        }
    }
}