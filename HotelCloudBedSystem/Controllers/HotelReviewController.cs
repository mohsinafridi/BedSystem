using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelCloudBedSystem.Controllers
{ 
    public class HotelReviewController : Controller
    {
        private HotelCloudDbContext _context;

        public HotelReviewController(HotelCloudDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            HotelDetailViewModel model = new HotelDetailViewModel();

            int totalprice = 0;
            int avergaeprice = 0;
            int totalstar = 0;
            int averagestar = 0;
            if (id == 0)
            {

            }

            var hotel = _context.hotels.FirstOrDefault(p => p.HotelId == id);
            var hotelreviews = _context.hotelReviews.Include(p => p.hotel).
                Where(p => p.hotel.HotelId == hotel.HotelId).ToList();

            var rooms = _context.hotelRooms.Include(p => p.Hotel).
                Where(p => p.Hotel.HotelId == hotel.HotelId).ToList();

            if (rooms != null)
            {
                for (int room = 0; room < rooms.Count; room++)
                {
                    totalprice = totalprice + rooms[room].RsPernight;
                }
                avergaeprice = totalprice / rooms.Count;
            }


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

            model.HotelId = hotel.HotelId;
            model.HotelCity = hotel.HotelCity;
            model.HotelName = hotel.HotelName;
            model.Description = hotel.Description;
            model.Address = hotel.Address;
            model.HotelImage = hotel.HotelImage;
            model.ReviewStar = averagestar;
            model.AveragePrice = avergaeprice;
            model.TotalReview = hotelreviews.Count;
            return View(model);
           
        }
    }
}