using HotelCloudBedSystem.Areas.Manager.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.ViewComponents
{
    public class ManagerDashBoardCountViewComponent:ViewComponent
    {
        private IEFRepository _repository;
        private UserManager<AppUser> _userManager;

        public ManagerDashBoardCountViewComponent(IEFRepository repository,
            UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new ManagerDashboardCountViewModel();
            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;

            if (OnlineUser != null && _userManager.IsInRoleAsync(OnlineUser, "Manager").Result)
            {
                var hotel = _repository.GetHotelByManagerId(OnlineUser.Id, true);
                if(hotel != null)
                {
                    model.TotalFloors = _repository.FloorsCount(hotel.HotelId);
                    model.hotelTotalRooms = _repository.HotelRoomsCount(hotel.HotelId);
                    model.HotelBookedRooms = _repository.HotelBookedRoom(hotel.HotelId);
                    model.HotelNotBookedRooms = _repository.HotelNotBooked(hotel.HotelId);
                    model.HotelPaymentCount = _repository.HotelTotalPaymentInNumbers(hotel.HotelId);
                    model.HotelTotalReservation = _repository.HotelReservationCount(hotel.HotelId);
                }
            }
                return View(model);
        }
    }
}
