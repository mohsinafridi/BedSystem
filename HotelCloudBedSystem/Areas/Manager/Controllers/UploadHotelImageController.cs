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
    public class UploadHotelImageController : Controller
    {
        private HotelCloudDbContext _context;

        public UploadHotelImageController(HotelCloudDbContext context)
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
            var model = new HotelImagesViewModel()
            {
                HotelId = id
            };

            return PartialView(model);
        }


        [HttpPost]
        public IActionResult _UploadImage(HotelImagesViewModel model)
        {
            if (!ModelState.IsValid)
            {

            }
            var hotelimage = new HotelImages();
            
            using (var memoryStream = new MemoryStream())
            {

                model.hotelimage.CopyToAsync(memoryStream);
                hotelimage.HImage = memoryStream.ToArray();

            }

            hotelimage.Hotel = _context.hotels.FirstOrDefault(p => p.HotelId == model.HotelId);

            _context.Add(hotelimage);
            _context.SaveChanges();

            return RedirectToAction("Index", new { area = "Manager", controller = "HotelProfile" });
        }

    }
}