using HotelCloudBedSystem.Areas.Manager.ViewModels;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelCloudBedSystem.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class ManagerProfileController:Controller
    {
        private UserManager<AppUser> _userManager;

        public ManagerProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var model = new ManagerProfileViewModel();
            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;
            if (OnlineUser != null && _userManager.IsInRoleAsync(OnlineUser, "Manager").Result)
            {
                model.FirstName = OnlineUser.FirstName;
                model.LastName = OnlineUser.LastName;
                model.Email = OnlineUser.Email;
                model.AvatarImage = OnlineUser.AvatarImage;
                model.IsEnabled = OnlineUser.IsEnable;
                model.EmailConfirmed = OnlineUser.EmailConfirmed;
                model.PhoneNo = OnlineUser.PhoneNumber;
                //model.role = OnlineUser.AppRole.Name;
                model.Address = OnlineUser.Address;
                model.AboutYou = OnlineUser.Aboutyou;
            }


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ManagerProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;

            if (OnlineUser != null && _userManager.IsInRoleAsync(OnlineUser, "Manager").Result)
            {
                OnlineUser.FirstName = model.FirstName;
                OnlineUser.LastName = model.LastName;
                OnlineUser.Email = model.Email;
                OnlineUser.Address = model.Address;
                OnlineUser.Aboutyou = model.AboutYou;
                OnlineUser.PhoneNumber = model.PhoneNo;
            }

            var result = _userManager.UpdateAsync(OnlineUser).Result;

            if (result.Succeeded)
            {
                return RedirectToAction("Index", new { area = "Manager", controller = "ManagerProfile" });
            }

            return View();

        }

       

    }
}
