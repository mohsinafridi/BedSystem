using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelCloudBedSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminProfileController : Controller
    {
        private UserManager<AppUser> _userManager;

        public AdminProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var model = new AdminProfileViewModel();
            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;
            if (OnlineUser != null && _userManager.IsInRoleAsync(OnlineUser, "Admin").Result)
            {
                model.FirstName = OnlineUser.FirstName;
                model.LastName = OnlineUser.LastName;
                model.Email = OnlineUser.Email;
                model.AvatarImage = OnlineUser.AvatarImage;
                model.IsEnabled = OnlineUser.IsEnable;
                model.EmailConfirmed = OnlineUser.EmailConfirmed;
                model.PhoneNo = OnlineUser.PhoneNumber;
                model.role = OnlineUser.AppRole.Name;
                model.Address = OnlineUser.Address;
                model.Aboutyou = OnlineUser.Aboutyou;
            }


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(AdminProfileViewModel model)
        {

            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;


            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (OnlineUser != null && _userManager.IsInRoleAsync(OnlineUser, "Admin").Result)
            {
                OnlineUser.FirstName = model.FirstName;
                OnlineUser.LastName = model.LastName;
                OnlineUser.Email = model.Email;
                OnlineUser.Address = model.Address;
                OnlineUser.Aboutyou = model.Aboutyou;
                OnlineUser.PhoneNumber = model.PhoneNo;
            }

            var result = _userManager.UpdateAsync(OnlineUser).Result;

            if (result.Succeeded)
            {
                return RedirectToAction("Index", new { area = "Admin", controller = "AdminProfile" });
            }
            return View();
        }
    }
}
