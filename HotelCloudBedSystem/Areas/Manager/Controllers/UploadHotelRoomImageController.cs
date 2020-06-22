using HotelCloudBedSystem.Areas.Manager.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace HotelCloudBedSystem.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class UploadHotelRoomImageController : Controller
    {
        private HotelCloudDbContext _context;

        public UploadHotelRoomImageController(HotelCloudDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
           
            return View();
        }


        [HttpGet]
        public IActionResult _UploadImage(int id)
        {
            if (id == 0)
            {

            }
            var model = new HotelRoomImagesViewModel()
            {
                HotelRoomId = id
            };

            return PartialView(model);
        }


        [HttpPost]
        public IActionResult _UploadImage(HotelRoomImagesViewModel model)
        {
            if (!ModelState.IsValid)
            {

            }
            var roomimage = new RoomsImage();
            
            using (var memoryStream = new MemoryStream())
            {

                model.hotelRoomimage.CopyToAsync(memoryStream);
                roomimage.images = memoryStream.ToArray();

            }
              
            roomimage.HotelRoom = _context.hotelRooms.FirstOrDefault(p => p.HotelRoomId == model.HotelRoomId);

            _context.Add(roomimage);
            _context.SaveChanges();

            return RedirectToAction("Index", new { area = "Manager", controller = "HotelRoomProfile" ,id=model.HotelRoomId });
        }

    }
}