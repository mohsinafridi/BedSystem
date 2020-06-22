using HotelCloudBedSystem.Areas.User.ViewModels;
using HotelCloudBedSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HotelCloudBedSystem.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class HotelReviewController : Controller
    {
        private HotelCloudDbContext _context;

        public HotelReviewController(HotelCloudDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {

            AddReviewViewModel model = null;
            List<AddReviewViewModel> List = new List<AddReviewViewModel>();
            if(id == null)
            {

            }

            var reviews = _context.hotelReviews.Include(p => p.hotel)
                
                .Where(p => p.hotel.HotelId == id).ToList();

            if(reviews == null)
            {

            }

            foreach(var rev in reviews)
            {
                model = new AddReviewViewModel()
                {
                    UserName=rev.UserName,
                    Review=rev.Review,
                    ReviewStar=rev.ReviewStar,
                    HotelId=rev.hotel.HotelId
                   
                    
                };

                List.Add(model);
            }

            
            return View(List);
        }
    }
}