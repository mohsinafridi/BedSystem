using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HotelCloudBedSystem.Areas.User.ViewModels;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelCloudBedSystem.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles ="User")]
    public class HomeController : Controller
    {
        private UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var model = new UserProfileViewModel();
            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;
            if (OnlineUser != null && _userManager.IsInRoleAsync(OnlineUser, "User").Result)
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
        public IActionResult Index(UserProfileViewModel model)
        {

            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;


            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (OnlineUser != null && _userManager.IsInRoleAsync(OnlineUser, "User").Result)
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
                return RedirectToAction("Index", new { area = "User", controller = "Home" });
            }
            return View();
        }

        [HttpGet]
        public IActionResult _UpdateProfilePic()
        {
            UserProfilePictureUpdateViewModel model = new UserProfilePictureUpdateViewModel();
            var users = _userManager.GetUsersInRoleAsync("User").Result;
            if (User != null)
            {
                foreach (var user in users)
                {
                    var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;

                    if (user == OnlineUser)
                    {
                        model.AvatarImages = user.AvatarImage;
                    }
                }
            }
            return PartialView(model);
        }

        [HttpPost]

        public IActionResult _UpdateProfilePic(UserProfilePictureUpdateViewModel model)
        {

            if (ModelState.IsValid)
            {
                var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;
                if (OnlineUser != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        model.avatarImages.CopyToAsync(memoryStream);
                        OnlineUser.AvatarImage = memoryStream.ToArray();

                    }

                    var result = _userManager.UpdateAsync(OnlineUser).Result;

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", new { area = "User", controller = "Home" });
                    }
                }

            }
            return PartialView();
        }
    }
}