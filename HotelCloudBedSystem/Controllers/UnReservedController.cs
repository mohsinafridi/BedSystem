using System;
using System.Linq;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelCloudBedSystem.Controllers
{
    public class UnReservedController : Controller
    {
        private HotelCloudDbContext _context;
        private IEmailSender _emailSender;
        private ISmsSender _msSender;

        public UnReservedController(HotelCloudDbContext context,
              IEmailSender emailSender,
            ISmsSender msSender)
        {
            _context = context;
            _emailSender = emailSender;
            _msSender = msSender;
        }
        public IActionResult Index()
        {

            var reservations = _context.roomReservations.Include(p => p.Hotelroom)
                .Include(p => p.Hotelroom.Hotel)
                .Where(p => p.IsActive == true && p.IsPaymentSuccessfull == true)
                .Where(p => p.Hotelroom.IsBooked == true).ToList();

            if(reservations == null)
            {
                return NotFound();
            };

            foreach(var res in reservations)
            {
                if(res.ChkOutdate.Date == DateTime.Now.Date)
                {
                    res.IsActive = false;
                    _context.Update(res);
                    _context.SaveChanges();

                    var room = _context.hotelRooms.Include(p => p.Hotel)
                        .FirstOrDefault(p => p.HotelRoomId == res.Hotelroom.HotelRoomId);

                    if(room == null)
                    {
                        return NotFound();
                    }

                    room.IsBooked = false;

                    
                    _context.Update(room);
                    _context.SaveChanges();

                    _emailSender.SendEmailAsync(res.Email, "Unreserved room",
                     $"Your reservation time is completed ....thanks for coperating");

                    _msSender.SendSmsAsync(res.PhoneNo, "Unreserved" +
                        "Your reservation time is completed ....thanks for coperating");
                };
            }

            return View();
        }
    }
}