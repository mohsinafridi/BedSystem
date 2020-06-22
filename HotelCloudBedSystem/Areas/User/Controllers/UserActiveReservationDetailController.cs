using System.Linq;
using HotelCloudBedSystem.Areas.User.ViewModels;
using HotelCloudBedSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelCloudBedSystem.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class UserActiveReservationDetailController : Controller
    {
        private HotelCloudDbContext _context;

        public UserActiveReservationDetailController(HotelCloudDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {

            var ActiveReservationDetail = _context.roomReservations.Include(p => p.Hotelroom)
                .Include(p => p.Hotelroom.Hotel).Include(p => p.Hotelroom.Hotel.AppUser)
                .FirstOrDefault(p => p.RoomReservationId == id);

            if(ActiveReservationDetail == null)
            {
                return NotFound("ReservationDetailNotFouns");
            }

            UserReservationViewModel model = new UserReservationViewModel()
            {
                RoomName = ActiveReservationDetail.Hotelroom.RoomName,
                RoomNo = ActiveReservationDetail.Hotelroom.RoomNo,
                HotelCity = ActiveReservationDetail.Hotelroom.Hotel.HotelCity,
                HotelName = ActiveReservationDetail.Hotelroom.Hotel.HotelName,
                CheckIn = ActiveReservationDetail.ChkIndate,
                CheckOut = ActiveReservationDetail.ChkOutdate,
                NoOfNight = ActiveReservationDetail.NoOfNight,
                ReservationId = ActiveReservationDetail.RoomReservationId,
                UserId = ActiveReservationDetail.Hotelroom.Hotel.AppUser.Id,
                UserName=ActiveReservationDetail.UserName,
                RoomId=ActiveReservationDetail.Hotelroom.HotelRoomId,
                RoomImage=ActiveReservationDetail.Hotelroom.RoomImage
            };

            return View(model);
        }
    }
}