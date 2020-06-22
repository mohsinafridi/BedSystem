using System;
using System.Collections.Generic;
using System.Linq;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using HotelCloudBedSystem.Services;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace HotelCloudBedSystem.Controllers
{
    public class HotelPaymentController : Controller
    {
        private HotelCloudDbContext _context;
        private IEmailSender _emailSender;
        private ISmsSender _smsSender;

        public HotelPaymentController(HotelCloudDbContext context,
             IEmailSender emailSender,
            ISmsSender smsSender)
        {
            _context = context;
            _emailSender = emailSender;
            _smsSender = smsSender;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Payment(int id)
        {
            int totalstar = 0;
            int averagestar = 0;
            var Res = _context.roomReservations.Include(p => p.Hotelroom)
                .Include(p => p.Hotelroom.Hotel)
                .FirstOrDefault(p => p.RoomReservationId == id);
            if (Res == null)
            {
                return NotFound();
            }
            var hotelreviews = _context.hotelReviews.Include(p => p.hotel).
               Where(p => p.hotel.HotelId == Res.Hotelroom.Hotel.HotelId).ToList();
            if (hotelreviews != null)
            {
                for (int review = 0; review < hotelreviews.Count; review++)
                {
                    totalstar = totalstar + hotelreviews[review].ReviewStar;
                }
                averagestar = totalstar / hotelreviews.Count;
            }
            TimeSpan diff = Res.ChkOutdate.Date - Res.ChkIndate.Date;
            PaymentViewModel model = new PaymentViewModel()
            {
                hotelName = Res.Hotelroom.Hotel.HotelName,
                HotelCity = Res.Hotelroom.Hotel.HotelCity,
                HotelAddress = Res.Hotelroom.Hotel.Address,
                Price = Res.Hotelroom.RsPernight,
                TotalPrice = diff.Days * (Res.Hotelroom.RsPernight),
                NoOfDays = diff.Days,
                UserCity = Res.UserCity,
                ZipCode = Res.ZipCode,
                UserName = Res.UserName,
                Address = Res.Address,
                Cnic = Res.Cnic,
                PhoneNo = Res.PhoneNo,
                Email = Res.Email,
                HotelStar = averagestar,
                Roomname = Res.Hotelroom.RoomName,
                RoomNo = Res.Hotelroom.RoomNo,
                ChkIndate = Res.ChkIndate,
                ChkOutdate = Res.ChkOutdate,
                ReservationId = id
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Payment(PaymentViewModel model,
            string stripeToken, string stripeEmail)
        {

            var reservation = _context.roomReservations.Include(p => p.Hotelroom)
                .Include(p=>p.Hotelroom.Hotel)
                .FirstOrDefault(p => p.RoomReservationId == model.ReservationId);

            Dictionary<string, string> Metadata = new Dictionary<string, string>();
            Metadata.Add("Product", "RubberDuck");
            Metadata.Add("Quantity", "10");
            var options = new ChargeCreateOptions
            {
                Amount = model.TotalPrice * 100,
                Currency = "PKR",
                Description = "room reservation",
                SourceId = stripeToken,
                ReceiptEmail = stripeEmail,
                Metadata = Metadata
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);

            if (charge.Status.ToLower() == "succeeded")
            {
                reservation.IsActive = true;
                reservation.IsPaymentSuccessfull = true;
                _context.Update(reservation);
                _context.SaveChanges();

                var payment = new Payment()
                {
                    PaymentType = "debit card",
                    RoomReservation = reservation,
                    Amount = model.TotalPrice * 100
                };

                _context.Add(payment);
                if (_context.SaveChanges() > 0)
                {
                    _emailSender.SendEmailAsync(reservation.Email, $"Payment Successfully Payed for Hotel  {reservation.Hotelroom.Hotel.HotelName} Room {reservation.Hotelroom.RoomName} Total Amount Payes {model.TotalPrice * 100}",
                   $"Now Enjoy your Time In Hotel");

                    _smsSender.SendSmsAsync(reservation.PhoneNo, $"Payment Successfully Payed for Hotel {reservation.Hotelroom.Hotel.HotelName} Room {reservation.Hotelroom.RoomName} Total Amount Payes {model.TotalPrice * 100}"   +
                       "Now Enjoy your Time In Hotel");
                    return RedirectToAction("SuccessfullpyamentMessage", new
                    {
                        area = "",
                        controller = "HotelPayment"
                    });


                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult SuccessfullpyamentMessage()
        {
            return View();
        }


    }
}
