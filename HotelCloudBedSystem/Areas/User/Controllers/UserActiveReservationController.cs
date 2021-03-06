﻿using HotelCloudBedSystem.Areas.User.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HotelCloudBedSystem.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class UserActiveReservationController : Controller
    {
        private HotelCloudDbContext _context;
        private UserManager<AppUser> _userManager;

        public UserActiveReservationController(HotelCloudDbContext context ,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            UserReservationViewModel model = null;
            List<UserReservationViewModel> List = new List<UserReservationViewModel>();
            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;
            if (OnlineUser != null && _userManager.IsInRoleAsync(OnlineUser, "User").Result)
            {
                var Reservations = _context.roomReservations.Include(p => p.Hotelroom)
                    .Include(p=>p.Hotelroom.Hotel)
                    .Where(p => p.Email == OnlineUser.Email)
                    .Where(p => p.IsActive == true).ToList();

                if(Reservations == null)
                {

                }

                foreach(var res in Reservations)
                {
                    model = new UserReservationViewModel()
                    {
                        RoomName=res.Hotelroom.RoomName,
                        RoomNo=res.Hotelroom.RoomNo,
                        HotelCity=res.Hotelroom.Hotel.HotelCity,
                        HotelName=res.Hotelroom.Hotel.HotelName,
                        CheckIn=res.ChkIndate,
                        CheckOut=res.ChkOutdate,
                        NoOfNight=res.NoOfNight,
                        ReservationId=res.RoomReservationId,
                        UserId=OnlineUser.Id,
                        RoomId=res.Hotelroom.HotelRoomId,
                        HotelId=res.Hotelroom.Hotel.HotelId
                    };

                    List.Add(model);
                }

            }
                return View(List);
        }
    }
}