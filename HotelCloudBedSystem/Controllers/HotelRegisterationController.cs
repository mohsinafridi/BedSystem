using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using HotelCloudBedSystem.Services;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Controllers
{
    public class HotelRegisterationController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IEFRepository _repository;
        private IEmailSender _emailSender;
        private ISmsSender _smsSender;

        public HotelRegisterationController(
            UserManager<AppUser> userManager,
            IEFRepository repository,
            IEmailSender emailSender,
            ISmsSender smsSender)
        {
            _userManager = userManager;
            _repository = repository;
            _emailSender = emailSender;
            _smsSender = smsSender;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HotelRegister(string email)
        {
            var Hotelmodel = new HotelRegisterationViewModel();
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user != null)
            {
                Hotelmodel.AppUser = user.FirstName + user.LastName;
                Hotelmodel.AppUserId = user.Id;
            }

            return View(Hotelmodel);

        }

        [HttpPost]
        public async  Task<IActionResult> HotelRegister(HotelRegisterationViewModel model)
        {
            var user = _userManager.FindByIdAsync(model.AppUserId).Result;
            if (ModelState.IsValid)
            {
                var hotel = new Hotel()
                {
                  HotelName=model.HotelName,
                  Description=model.Description,
                  AboutHotel=model.Description,
                  NoOfFloors=model.NoOfFloors,
                  NoOfRooms=model.NoOfRooms,
                  HotelCity=model.HotelCity,
                  ZipCode=model.ZipCode,
                  Address=model.Address,
                  AppUser= user

                };
                var city = new City()
                {
                    CityName = model.HotelCity
                };
                _repository.Add(city);
                 _repository.Add(hotel);

                if (_repository.SaveChange())
                {
                    await _emailSender.SendEmailAsync(user.Email, "Account Requestng  Mail",
                     $"Your Request for Adding Account is send Succesfully..it will took a few days process on it")
                   .ConfigureAwait(true);
                    await _smsSender.SendSmsAsync(user.PhoneNumber, "Your Request for Adding Account is send Succesfully." +
                        "it will took a few days process on it").ConfigureAwait(true);
                    return RedirectToAction("Message", "AddingHotelMessage", new { email = user.Email});
               
                }


            }

                return View();
        }
    }
}