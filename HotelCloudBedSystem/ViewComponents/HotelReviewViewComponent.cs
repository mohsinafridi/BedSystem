using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewComponents
{
    public class HotelReviewViewComponent:ViewComponent
    {
        private HotelCloudDbContext _context;

        public HotelReviewViewComponent(HotelCloudDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            HotelReviewViewModel model = null;
            List<HotelReviewViewModel> list = new List<HotelReviewViewModel>();

            var reviews = _context.hotelReviews.
                Include(p => p.hotel).Where(p => p.hotel.HotelId == id).ToList();

            if(reviews == null)
            {

            }

            foreach(var review in reviews)
            {
                model = new HotelReviewViewModel()
                {
                    Review=review.Review,
                    ReviewStar=review.ReviewStar,
                    UserName=review.UserName
                };
                list.Add(model);
            }
           
            return View(list);
        }

        }
}
