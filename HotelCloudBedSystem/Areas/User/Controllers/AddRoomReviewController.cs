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
    public class AddRoomReviewController : Controller
    {
        private HotelCloudDbContext _context;

        public AddRoomReviewController(HotelCloudDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index(int id)
        {

            if (id == null)
            {

            }
            var Ratings = _context.starRatings;

            

            AddReviewViewModel model = new AddReviewViewModel();

            model.Ratings = Ratings.Select(p => new SelectListItem()
            {
                Text = p.StarName,
                Value = p.StarRatingId.ToString()
            }).ToList();

            model.RoomId = id;
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
            RoomReview review = new RoomReview()
            {
                ReviewStar = reviewstar.StarNo,
                Review = model.Review,
                UserName = model.UserName,
                hotelRoom = _context.hotelRooms.FirstOrDefault(p => p.HotelRoomId
                == model.RoomId)
            };

            _context.Add(review);

           if( _context.SaveChanges() > 0)
            {
                return RedirectToAction("Index", new
                {
                    area = "User",
                    controller = "RoomReview",
                    id = model.RoomId
                });
            }

            return View();
        }

    }
}