using System.IO;
using System.Linq;
using HotelCloudBedSystem.Areas.Manager.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelCloudBedSystem.Areas.Manager.Controllers
{

    [Area("Manager")]
    [Authorize(Roles ="Manager")]
    public class HotelRoomProfilePictureUploadController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IEFRepository _repository;
        private HotelCloudDbContext _context;

        public HotelRoomProfilePictureUploadController(UserManager<AppUser> userManager
            ,IEFRepository repository,HotelCloudDbContext context)
        {
            _userManager = userManager;
            _repository = repository;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index(int id)
        {
            HotelRoomProfilePictureViewModel model = new HotelRoomProfilePictureViewModel();
            model.RoomId = id;
            return PartialView(model);
        }


        [HttpPost]
        public IActionResult Index(HotelRoomProfilePictureViewModel model)
        {
            if (ModelState.IsValid)
            {

                var room = _context.hotelRooms.FirstOrDefault(p => p.HotelRoomId == model.RoomId);
                    using (var memoryStream = new MemoryStream())
                    {
                        model.roomImages.CopyToAsync(memoryStream);
                        room.RoomImage = memoryStream.ToArray();

                    }

                    _repository.Update(room);

                    if (_repository.SaveChange())
                    {
                    return RedirectToAction("Index", new { area = "Manager", controller = "HotelRoomProfile" ,id=model.RoomId });
                }
                

            }
            return PartialView();

        }


    }
}