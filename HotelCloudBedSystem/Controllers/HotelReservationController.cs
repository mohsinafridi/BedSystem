using System;
using System.Linq;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using HotelCloudBedSystem.Services;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelCloudBedSystem.Controllers
{
    public class HotelReservationController : Controller
    {
        private HotelCloudDbContext _context;
        private IEmailSender _emailSender;
        private ISmsSender _smsSender;

        public HotelReservationController(HotelCloudDbContext context,
            IEmailSender emailSender,
            ISmsSender smsSender)
        {
            _context = context;
            _emailSender = emailSender;
            _smsSender = smsSender;
        }
       
        [HttpGet]
        public IActionResult Hotelreservtion(RoomReservationViewModel mOdel,int no)
        {
            int totalstar = 0;
            int averagestar = 0;
            if(mOdel == null)
            {
                return null;
            }

            var room = _context.hotelRooms.Include(p=>p.Hotel)
                .FirstOrDefault(p => p.HotelRoomId == mOdel.RoomId);
            if (room == null)
            {

            }

            var hotel = _context.hotels.FirstOrDefault(p => p.HotelId == room.Hotel.HotelId);
            var hotelreviews = _context.hotelReviews.Include(p => p.hotel).
               Where(p => p.hotel.HotelId == hotel.HotelId).ToList();
            if (hotelreviews != null)
            {
                for (int review = 0; review < hotelreviews.Count; review++)
                {
                    totalstar = totalstar + hotelreviews[review].ReviewStar;
                }
                averagestar = totalstar / hotelreviews.Count;
            }

            if (hotel == null)
            {

            }
            RoomReservationViewModel model = new RoomReservationViewModel()
            {
                RoomId = room.HotelRoomId,
                HotelId = hotel.HotelId,
                Price=room.RsPernight,
                Roomname=room.RoomName,
                HotelCity=hotel.HotelCity,
                HotelName=hotel.HotelName,
                HotelAddress=hotel.Address,
                HotelStar= averagestar,
                ChkIndate=mOdel.ChkIndate,
                ChkOutdate=mOdel.ChkOutdate

            };
            return View(model);

            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Hotelreservtion(RoomReservationViewModel model)
        {
            
            int totalPaymentAmount = 0;
            if (!ModelState.IsValid)
            {

            }

            if(model.ChkIndate == null && model.ChkOutdate== null)
            {
              
            }

            TimeSpan difference = model.ChkOutdate.Date - model.ChkIndate.Date;
            
            var room = _context.hotelRooms.FirstOrDefault(p => p.HotelRoomId == model.RoomId);
            RoomReservation reservation = new RoomReservation()
            {
                UserName = model.UserName,
                ChkIndate = model.ChkIndate,
                ChkOutdate = model.ChkOutdate,
                Email=model.Email,
                Cnic = model.Cnic,
                PhoneNo = model.PhoneNo,
                Address = model.Address,
                Hotelroom = room,
                UserCity=model.UserCity,
                ZipCode=model.ZipCode,
                NoOfNight= difference.Days

            };

            _context.Add(reservation);

            if(_context.SaveChanges() > 0)
            {
                int id = 0;
                var Res = _context.roomReservations.Include(p => p.Hotelroom)
                    .Include(p=>p.Hotelroom.Hotel)
                   .FirstOrDefault(p => p.IsActive == false && p.PhoneNo == model.PhoneNo
                   && p.UserCity == model.UserCity && p.UserName == model.UserName
                   && p.Cnic == model.Cnic && p.Email == model.Email);

                 //_emailSender.SendEmailAsync(Res.Email, $"Reservation",
                 //   $" Your Reservation Completed Successfuly for Hotel {Res.Hotelroom.Hotel.HotelName} Room {Res.Hotelroom.RoomName} Now Contiue with Payment");

                _smsSender.SendSmsAsync(Res.PhoneNo, $"(From Hotel Network)" +
                   $"Your Reservation Completed Successfuly for (Hotel) ::  {Res.Hotelroom.Hotel.HotelName}  (Room) :: {Res.Hotelroom.RoomName} ... Now Proccessed further with Payment");

                if (Res == null)
                {
                    
                }
                id = Res.RoomReservationId;
                return RedirectToAction("Payment", "HotelPayment", new { id = id });
            }
            

            return View();
        }



    }
}