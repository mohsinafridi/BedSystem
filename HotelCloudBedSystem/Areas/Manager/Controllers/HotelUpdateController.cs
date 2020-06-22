using HotelCloudBedSystem.Areas.Manager.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;



namespace HotelCloudBedSystem.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class HotelUpdateController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IEFRepository _repository;
        private HotelCloudDbContext _context;

        public HotelUpdateController(UserManager<AppUser> userManager,
            IEFRepository repository, HotelCloudDbContext context)
        {
            _userManager = userManager;
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;

            if (OnlineUser != null && _userManager.IsInRoleAsync(OnlineUser, "Manager").Result)
            {
                var hotel = _repository.GetHotelByManagerId(OnlineUser.Id, true);
                var facilities = _context.HotelFacilities.FirstOrDefault(p =>
                p.Hotel.HotelId == hotel.HotelId);
                if (hotel != null)
                {
                    var model = new HotelListViewModel()
                    {
                        HotelId = hotel.HotelId,
                        HotelName = hotel.HotelName,
                        HotelCity = hotel.HotelCity,
                        NoOfFloors = hotel.NoOfFloors,
                        NoOfRooms = hotel.NoOfRooms,
                        Address = hotel.Address,
                        Description = hotel.Description,
                        FreeWifi=facilities.FreeWifi,
                        Dinner=facilities.Dinner,
                        Laundry=facilities.Laundry,
                        Lunch=facilities.Lunch,
                        AttachedWashrooms=facilities.AttachedWashrooms,
                        Receptionservices=facilities.Receptionservices,
                        CarParking=facilities.CarParking


                    };
                    return PartialView(model);
                }
            }
            return PartialView();
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Index(HotelListViewModel model)
        {
            bool Status = false;
            string Message = string.Empty;
            var hotel = _repository.HotelById(model.HotelId);
            var facilities = _context.HotelFacilities.FirstOrDefault(p =>
              p.Hotel.HotelId == hotel.HotelId);
            if (ModelState.IsValid)
            {
                hotel.HotelName = model.HotelName;
                hotel.HotelId = model.HotelId;
                hotel.HotelCity = model.HotelCity;
                hotel.NoOfFloors = model.NoOfFloors;
                hotel.NoOfRooms = model.NoOfRooms;
                hotel.Address = model.Address;
                hotel.Description = model.Description;
               


                _repository.Update(hotel);

                if (_repository.SaveChange())
                {
                    if(facilities == null)
                    {
                        var hotelfacility = new HotelFacilities()
                        {
                            FreeWifi=model.FreeWifi,
                            Dinner=model.Dinner,
                            CarParking=model.CarParking,
                            AttachedWashrooms=model.AttachedWashrooms,
                            Receptionservices=model.Receptionservices,
                            Laundry=model.Laundry,
                            Lunch=model.Lunch,
                            BreckFast=model.BreckFast,
                        };
                        _context.Add(hotelfacility);
                        _context.SaveChanges();
                    }
                    else
                    {
                        facilities.FreeWifi = model.FreeWifi;
                        facilities.Dinner = model.Dinner;
                        facilities.CarParking = model.CarParking;
                        facilities.AttachedWashrooms = model.AttachedWashrooms;
                        facilities.Receptionservices = model.Receptionservices;
                        facilities.Laundry = model.Laundry;
                        facilities.Lunch = model.Lunch;
                        facilities.BreckFast = model.BreckFast;

                        _context.Update(facilities);
                        _context.SaveChanges();
                    }
                   

                    return RedirectToAction("Index", new { area = "Manager", controller = "HotelProfile" });
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

    }
}
