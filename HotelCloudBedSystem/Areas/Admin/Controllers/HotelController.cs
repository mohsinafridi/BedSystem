using HotelCloudBedSystem.Areas.Admin.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HotelController : Controller
    {
        private IEFRepository _repository;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public HotelController(IEFRepository repository, UserManager<AppUser> userManager
            , RoleManager<AppRole> roleManager)
        {
            _repository = repository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index(QueryOptions options, HotelSearchedByNameViewModel model)
        {
            return View(_repository.Hotels(options, model.Hotelid, true));
        }
        [HttpGet]
        public IActionResult _AddHotel()
        {
            int count = 0;

            AddHotelViewModel model = null;

            var usersOfRole = _userManager.GetUsersInRoleAsync("Manager").Result;

            if (usersOfRole != null)
            {
                model = new AddHotelViewModel()
                {
                    AppUser = usersOfRole.Select(p => new SelectListItem()
                    {
                        Text = p.FirstName + p.LastName,
                        Value = p.Id
                    }).ToList(),

                };
            }

            return PartialView(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult _AddHotel(AddHotelViewModel model)
        {
            bool Status = false;
            string Message = string.Empty;
            if (ModelState.IsValid)
            {
                var hotel = new Hotel()
                {
                    HotelName = model.HotelName,
                    HotelCity = model.HotelCity,
                    NoOfFloors = model.NoOfFloors,
                    NoOfRooms = model.NoOfRooms,
                    AppUser = _userManager.FindByIdAsync(model.AppUserId).Result
                };

                _repository.Add(hotel);

                if (_repository.SaveChange())
                {

                    return RedirectToAction("Index", new { area = "Admin", controller = "Hotel" });
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
            return Json(new { status = Status, message = Message });
        }


        [HttpGet]
        public IActionResult HotelDetailList(int id)
        {
            var model = new HotelListViewModel();
            var hotel = _repository.GetHotelById(id, true);

            if(hotel == null)
            {

            }

            model.HotelId = hotel.HotelId;
            model.HotelName = hotel.HotelName;
            model.NoOfFloors = hotel.NoOfFloors;
            model.NoOfRooms = hotel.NoOfRooms;
            model.HotelCity = hotel.HotelCity;
            model.Address = hotel.Address;
            model.Description = hotel.Description;
            //model.Manager = hotel.AppUser.FirstName + hotel.AppUser.LastName;
           
            return View(model);
        }


        [HttpGet]
        public IActionResult DeleteHotel(int id)
        {
            var result = _repository.HotelById(id);
            if(result != null)
            {
               _repository.Delete(result);

                if (_repository.SaveChange())
                {
                    return RedirectToAction("Index", new { area = "Admin", controller = "Hotel" });
                }
                
            }
            return View();
        }






    }
}
