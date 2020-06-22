using System.Collections.Generic;
using System.Linq;
using HotelCloudBedSystem.Areas.User.ViewModels;
using HotelCloudBedSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelCloudBedSystem.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class RoomReviewController : Controller
    {
        private HotelCloudDbContext _context;

        public RoomReviewController(HotelCloudDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {

            AddReviewViewModel model = null;
            List<AddReviewViewModel> List = new List<AddReviewViewModel>();
            if (id == null)
            {

            }

            var reviews = _context.roomReviews.Include(p => p.hotelRoom)
                .Include(p=>p.hotelRoom.Hotel)
                .Where(p => p.hotelRoom.HotelRoomId == id).ToList();

            if (reviews == null)
            {

            }

            foreach (var rev in reviews)
            {
                model = new AddReviewViewModel()
                {
                    UserName = rev.UserName,
                    Review = rev.Review,
                    ReviewStar = rev.ReviewStar,
                    RoomId=rev.hotelRoom.HotelRoomId

                };

                List.Add(model);
            }


            return View(List);
        }
    }
}