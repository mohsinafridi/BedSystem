using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles="Manager")]
    public class ReservationController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IEFRepository _repository;

        public ReservationController(UserManager<AppUser> userManager, IEFRepository repository)
        {
            _userManager = userManager;
            _repository = repository;

        }


        [HttpGet]
        public IActionResult Index(QueryOptions options)
        {
            var hotel = FindHotel();
            if (hotel != null)
            {
                return View(_repository.ReservationHistory(options, hotel.HotelId, 0, 0, 0, true));
            }
            return View();
        }


        #region FindHotel
        public Hotel FindHotel()
        {
            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;

            if (OnlineUser == null && !(_userManager.IsInRoleAsync(OnlineUser, "Manager").Result))
            {

            }
            return _repository.GetHotelByManagerId(OnlineUser.Id, true);
        }

        #endregion
    }
}
