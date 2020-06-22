using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewComponents
{
    public class HotelFilteringViewComponent : ViewComponent
    {
        private HotelCloudDbContext _context;

        public HotelFilteringViewComponent(HotelCloudDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(string city, int id,
            DateTime checkIn , DateTime checkout)
        {
            HotelSerachViewModel model = null;



            var Ratings = _context.starRatings;
            model = new HotelSerachViewModel()
            {
                Ratings = Ratings.Select(p => new SelectListItem()
                {
                    Text = p.StarName,
                    Value = p.StarRatingId.ToString()
                }).ToList(),

                City=city,
                hotelRoomTypeId=id,
                checkIn=checkIn,
                checkOut=checkout

            };

          
            return View(model);
        }
    }
}
