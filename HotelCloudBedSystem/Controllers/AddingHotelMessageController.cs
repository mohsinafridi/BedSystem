using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelCloudBedSystem.Controllers
{
    public class AddingHotelMessageController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IEFRepository _repository;

        public AddingHotelMessageController(UserManager<AppUser> userManager, IEFRepository repository)
        {
            _userManager = userManager;
            _repository = repository;

        }
        [HttpGet]
        public IActionResult Message(string email)
        {


            var user = _userManager.FindByEmailAsync(email).Result;
            var hotel = _repository.GetHotelByUserId(user.Id,true);

            if (user == null && hotel == null)
            {
            }

            var model = new HotelAndManagerViewModel()
            {
                ManagerName = user.FirstName + user.LastName,
                Address = user.Address,
                Email = user.Email,
                PhoneNo = user.PhoneNumber,
                HotelName = hotel.HotelName,
                City = hotel.HotelCity,
                Rooms = hotel.NoOfRooms,
                Floors = hotel.NoOfFloors,
            };


            return View(model);
            
        }
    }
}