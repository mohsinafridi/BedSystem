using HotelCloudBedSystem.Areas.User.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace HotelCloudBedSystem.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class AddHotelReviewController : Controller
    {
        private HotelCloudDbContext _context;

        public AddHotelReviewController(HotelCloudDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index(int id)
        {
            var Ratings = _context.starRatings;

            if (id == null)
            {

            }

            AddReviewViewModel model = new AddReviewViewModel();
            model.Ratings = Ratings.Select(p => new SelectListItem()
            {
                Text = p.StarName,
                Value = p.StarRatingId.ToString()
            }).ToList();

            model.HotelId = id;
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(AddReviewViewModel model)
        {
            if (model == null)
            {

            }
            var reviewstar = _context.starRatings.
                FirstOrDefault(p => p.StarRatingId == model.ReviewId);
            HotelReview review = new HotelReview()
            {
                ReviewStar = reviewstar.StarNo,
                Review = model.Review,
                UserName = model.UserName,
                hotel = _context.hotels.FirstOrDefault(p => p.HotelId == model.HotelId)
            };

            _context.Add(review);
            if(_context.SaveChanges() > 0)
            {
                return RedirectToAction("Index", new { area = "User", controller = "HotelReview" ,
                    id=model.HotelId } );
            }

            return View();
        }


    }
}