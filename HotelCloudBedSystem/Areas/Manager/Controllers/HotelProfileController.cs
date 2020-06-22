using HotelCloudBedSystem.Areas.Manager.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Migrations;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HotelCloudBedSystem.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles= "Manager")]
    public class HotelProfileController : Controller
    {
        private IEFRepository _repository;
        private UserManager<AppUser> _userManager;
        private HotelCloudDbContext _context;

        public HotelProfileController(IEFRepository repository, 
            UserManager<AppUser> userManager,HotelCloudDbContext context)
        {
            _repository = repository;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new HotelListViewModel();
            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;

            if (OnlineUser != null && _userManager.IsInRoleAsync(OnlineUser, "Manager").Result)
            {
                var hotel = _repository.GetHotelByManagerId(OnlineUser.Id, true);
                var facilities = _context.HotelFacilities.Include(p=>p.Hotel)
                    .FirstOrDefault(p => p.Hotel.HotelId == hotel.HotelId);
                if (hotel != null)
                {
                    model.HotelId = hotel.HotelId;
                    model.HotelName = hotel.HotelName;
                    model.NoOfFloors = hotel.NoOfFloors;
                    model.NoOfRooms = hotel.NoOfRooms;
                    model.HotelCity = hotel.HotelCity;
                    model.Address = hotel.Address;
                    model.HotelImage = hotel.HotelImage;
                    model.Description = hotel.Description;
                    
                    if(facilities != null)
                    {
                        model.FreeWifi = facilities.FreeWifi;
                        model.Dinner = facilities.Dinner;
                        model.Lunch = facilities.Lunch;
                        model.BreckFast = facilities.BreckFast;
                        model.Receptionservices = facilities.Receptionservices;
                        model.AttachedWashrooms = facilities.AttachedWashrooms;
                        model.CarParking = facilities.CarParking;
                        model.Laundry = facilities.Laundry;
                    }
                    model.Manager = hotel.AppUser.FirstName + hotel.AppUser.LastName;
                  
                    


                }
            }
            return View(model);
        }
    }
}
