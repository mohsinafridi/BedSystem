using System.IO;
using HotelCloudBedSystem.Areas.Manager.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelCloudBedSystem.Areas.Manager.Controllers
{

    [Area("Manager")]
    [Authorize(Roles ="Manager")]
    public class HotelProfilePictureUploadController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IEFRepository _repository;

        public HotelProfilePictureUploadController(UserManager<AppUser> userManager,IEFRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            HotelProfilePictureViewModel model = new HotelProfilePictureViewModel();
            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;

            if (OnlineUser != null && _userManager.IsInRoleAsync(OnlineUser, "Manager").Result)
            {
                model.AvatarImages = OnlineUser.AvatarImage;
            }

            return PartialView(model);
        }


        [HttpPost]
        public IActionResult Index(HotelProfilePictureViewModel model)
        {
            if (ModelState.IsValid)
            {
                var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;
                if (OnlineUser != null && _userManager.IsInRoleAsync(OnlineUser, "Manager").Result)
                {
                    var hotel = _repository.GetHotelByUserId(OnlineUser.Id, true);
                    using (var memoryStream = new MemoryStream())
                    {
                        model.avatarImages.CopyToAsync(memoryStream);
                        hotel.HotelImage = memoryStream.ToArray();

                    }

                    _repository.Update(hotel);

                    if (_repository.SaveChange())
                    {
                        return RedirectToAction("Index", new { area = "Manager", controller = "HotelProfile" });
                    }
                }

            }
            return PartialView();

        }


    }
}