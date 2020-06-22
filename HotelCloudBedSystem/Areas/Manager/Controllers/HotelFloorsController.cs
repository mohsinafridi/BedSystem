using HotelCloudBedSystem.Areas.Manager.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace HotelCloudBedSystem.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class HotelFloorsController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IEFRepository _repository;

        public HotelFloorsController(UserManager<AppUser> userManager, IEFRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index(QueryOptions options)
        {

            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;

            if (OnlineUser != null && _userManager.IsInRoleAsync(OnlineUser, "Manager").Result)
            {
                var hotel = _repository.GetHotelByManagerId(OnlineUser.Id, true);
                if (hotel != null)
                {
                    return View(_repository.FloorsByHotelId(options, hotel.HotelId, 0, true));

                }
            }
            return View();
        }


        #region Crud

        [HttpGet]
        public IActionResult Create()
        {
            int count = 0;

            FloorCreateViewModel model = null;

            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;

            if (OnlineUser != null && _userManager.IsInRoleAsync(OnlineUser, "Manager").Result)
            {
                var hotel = _repository.GetHotelByManagerId(OnlineUser.Id, true);
                if (hotel != null)
                {
                    model = new FloorCreateViewModel()
                    {
                        HotelId=hotel.HotelId,
                        HotelName=hotel.HotelName
                    };
                   
                }
            }

                return PartialView(model);
        }

        [HttpPost]
        public IActionResult Create(FloorCreateViewModel model)
        {
            bool Status = false;
            string Message = string.Empty;
            if (ModelState.IsValid)
            {
                var floor = new HotelFloors()
                {
                    FloorName = model.FloorName,
                    FloorNo = model.FloorNo,
                    NoOfRooms = model.NoOfRooms,
                    Hotel = _repository.HotelById(model.HotelId)

                };

                _repository.Add(floor);

                if (_repository.SaveChange())
                {

                    return RedirectToAction("Index", new { area = "Manager", controller = "HotelFloors" });
                }
                else
                {
                    Status = false;
                    Message = "Error inserting /Creating Course";
                }
            }
            else
            {
                ModelState.AddModelError("", "invalid / incomplete data");
            }
            //return Json(new { status = Status, message = Message });
            return View();
        }


        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            int count = 0;

            FloorCreateViewModel model = null;

            //var hotel = _repository.GetHotels();
            var floor = _repository.FloorById(id);
            if (floor != null)
            {
                model = new FloorCreateViewModel()
                {
                    HotelFloorsId = floor.HotelFloorsId,
                    FloorName = floor.FloorName,
                    FloorNo = floor.FloorNo,
                    NoOfRooms = floor.NoOfRooms,
                    //HotelId = floor.Hotel.HotelId
                };
            }

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Update(FloorCreateViewModel model)
        {
            bool Status = false;
            string Message = string.Empty;
            if (ModelState.IsValid)
            {
                var floor = new HotelFloors()
                {
                    FloorName = model.FloorName,
                    FloorNo = model.FloorNo,
                    NoOfRooms = model.NoOfRooms,
                    Hotel = _repository.HotelById(model.HotelId)

                };

                _repository.Update(floor);

                if (_repository.SaveChange())
                {

                    return RedirectToAction("Index", new { area = "Manager", controller = "HotelFloors" });
                }
                else
                {
                    Status = false;
                    Message = "Error inserting /Creating Course";
                }
            }
            else
            {
                ModelState.AddModelError("", "invalid / incomplete data");
            }
            //return Json(new { status = Status, message = Message });
            return View();


        }
        #endregion
        #endregion

    
    }
}


