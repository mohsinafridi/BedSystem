using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewComponents
{
    public class RoomFilteringViewComponent : ViewComponent
    {
        private HotelCloudDbContext _context;

        public RoomFilteringViewComponent(HotelCloudDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int hotelRoomtypeId,
            int hotelId ,DateTime checkIn, DateTime checkOut)
        {

            RoomSearchViewModel model = null;



            var Ratings = _context.starRatings;
            model = new RoomSearchViewModel()
            {
                Ratings = Ratings.Select(p => new SelectListItem()
                {
                    Text = p.StarName,
                    Value = p.StarRatingId.ToString()
                }).ToList(),

                hotelRoomTypeId= hotelRoomtypeId,
                HotelId=hotelId,
                

            };

            if(checkIn != null && checkOut != null)
            {
                model.CheckInDate = checkIn;
                model.CheckOutDate = checkOut;
            }
            return View(model);
        }
    }
}
