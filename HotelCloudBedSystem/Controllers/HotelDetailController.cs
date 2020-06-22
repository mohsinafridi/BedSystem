using System.Linq;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelCloudBedSystem.Controllers
{
    public class HotelDetailController : Controller
    {
        private HotelCloudDbContext _context;

        public HotelDetailController(HotelCloudDbContext context)
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
            var TotalRooms = _context.hotelRooms.Include(p => p.Hotel)
                .Where(p => p.Hotel.HotelId == hotel.HotelId).ToList();
            var bookedRooms = _context.hotelRooms.Include(p => p.Hotel)
                .Where(p => p.Hotel.HotelId == hotel.HotelId && p.IsBooked == true).Count();
            var FreeRooms = _context.hotelRooms.Include(p => p.Hotel)
               .Where(p => p.Hotel.HotelId == hotel.HotelId && p.IsBooked == false).Count();
            var hotelreviews = _context.hotelReviews.Include(p => p.hotel).
                Where(p => p.hotel.HotelId == hotel.HotelId).ToList();
            var hotelfacilities = _context.HotelFacilities.Include(p => p.Hotel)
                .FirstOrDefault(p => p.Hotel.HotelId == hotel.HotelId);

            if (TotalRooms != null)
            {
                for (int room = 0; room < TotalRooms.Count; room++)
                {
                    totalprice = totalprice + TotalRooms[room].RsPernight;
                }
                avergaeprice = totalprice / TotalRooms.Count;
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
            model.NoOfRooms = TotalRooms.Count;
            model.ReviewStar = averagestar;
            model.AveragePrice = avergaeprice;
            model.bookedRoomCount = bookedRooms;
            model.FreeRoomsCount = FreeRooms;
            model.TotalReview = hotelreviews.Count;

            if(hotelfacilities != null)
            {
                model.FreeWifi = hotelfacilities.FreeWifi;
                model.AttachedWashrooms = hotelfacilities.AttachedWashrooms;
                model.Dinner = hotelfacilities.Dinner;
                model.BreckFast = hotelfacilities.BreckFast;
                model.Lunch = hotelfacilities.Lunch;
                model.Receptionservices = hotelfacilities.Receptionservices;
                model.Laundry = hotelfacilities.Laundry;
                model.CarParking = hotelfacilities.CarParking;
            }
          

            return View(model);
        }
    }
}