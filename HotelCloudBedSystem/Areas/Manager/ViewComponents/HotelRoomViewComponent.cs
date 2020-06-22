using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.ViewComponents
{
    public class HotelRoomViewComponent : ViewComponent
    {
        private UserManager<AppUser> _userManager;
        private IEFRepository _repository;

        public HotelRoomViewComponent(UserManager<AppUser> userManager, IEFRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;

            if (OnlineUser != null && _userManager.IsInRoleAsync(OnlineUser, "Manager").Result)
            {
                var hotel = _repository.GetHotelByManagerId(OnlineUser.Id, true);
                if (hotel != null)
                {
                    return View(_repository.HotelRooms(hotel.HotelId, true));

                }

            }
            return View();
        }
    }
}
