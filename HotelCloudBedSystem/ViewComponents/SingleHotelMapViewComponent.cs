using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewComponents
{
    public class SingleHotelMapViewComponent : ViewComponent
    {
        private HotelCloudDbContext _context;

        public SingleHotelMapViewComponent(HotelCloudDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(string city, string Hotel)
        {
            HotelMaplocationViewModel model = null;

            int totalprice = 0;
            int avergaeprice = 0;
            int totalstar = 0;
            int averagestar = 0;
            int totalreview = 0;

            var CityLocation = _context.cityMapLocations.FirstOrDefault(p => p.CityName == city);

            var hotel = _context.hotels.
                FirstOrDefault(p => p.HotelName == Hotel);
            var hotellocation = _context.hotelMapLocations.
                Include(p => p.CityMapLocation).FirstOrDefault(p => p.HotelName == hotel.HotelName);
            var HotelRooms = _context.hotelRooms.
                Include(p => p.Hotel).
                Where(p => p.Hotel.HotelId == hotel.HotelId).ToList();
            if (HotelRooms != null)
            {
                for (int room = 0; room < HotelRooms.Count; room++)
                {
                    totalprice = totalprice + HotelRooms[room].RsPernight;
                }
                avergaeprice = totalprice / HotelRooms.Count;
            }

            var hotelreviews = _context.hotelReviews.Include(p => p.hotel).
                Where(p => p.hotel.HotelId == hotel.HotelId).ToList();
            if (hotelreviews != null)
            {
                for (int review = 0; review < hotelreviews.Count; review++)
                {
                    totalstar = totalstar + hotelreviews[review].ReviewStar;
                }
                averagestar = totalstar / hotelreviews.Count;
                totalreview = hotelreviews.Count;


            }
            model = new HotelMaplocationViewModel()
            {
                HotelName = hotellocation.HotelName,
                HotelLatitude = hotellocation.Latitude,
                HotelLongitude = hotellocation.Longitude,
                totalreviews = totalreview,
                AveragePrice = avergaeprice,
                AverageSatr = averagestar,
                CityName = CityLocation.CityName,
                CityLatitude = CityLocation.Latitude,
                CityLongitude = CityLocation.longtitude
            };
            return View(model);

        }





    }
}
