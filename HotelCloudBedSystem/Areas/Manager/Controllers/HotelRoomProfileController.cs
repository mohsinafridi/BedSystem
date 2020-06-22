using HotelCloudBedSystem.Areas.Manager.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HotelCloudBedSystem.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class HotelRoomProfileController : Controller
    {
        private IEFRepository _repository;
        private UserManager<AppUser> _userManager;
        private HotelCloudDbContext _context;

        public HotelRoomProfileController(IEFRepository repository,
            UserManager<AppUser> userManager, HotelCloudDbContext context)
        {
            _repository = repository;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var model = new HotelRoomListViewModel();


            var room = _context.hotelRooms.Include(p => p.Hotel).
                Include(p => p.HotelRoomType).Include(p => p.HotelFloors)
                .FirstOrDefault(p => p.HotelRoomId == id);
            if (room == null)
            {

            }

            var facilities = _context.RoomFacilities.Include(p => p.HotelRoom)
                .FirstOrDefault(p => p.HotelRoom.HotelRoomId == room.HotelRoomId);
            if (facilities != null)
            {
                model.FreeWifi = facilities.Internet;
                model.AttachedWashroom = facilities.AttachedWashRoom;
                model.Tv = facilities.Tv;
                model.Ac = facilities.Ac;
            }


            model.RoomId = room.HotelRoomId;
            model.RoomImage = room.RoomImage;
            model.RoomName = room.RoomName;
            model.RoomTye = room.HotelRoomType.RoomType;
            model.PerNightPrice = room.RsPernight;
            model.Description = room.Description;
            model.Isbooked = room.IsBooked;
            model.OccupancyLimit = room.OcupancyLimit;

            model.RoomNo = room.RoomNo;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(HotelRoomListViewModel model)
        {
            if (!ModelState.IsValid)
            {

            }

            var room = _context.hotelRooms.Include(p => p.Hotel)
                .FirstOrDefault(p => p.HotelRoomId == model.RoomId);
            var facilities = _context.RoomFacilities.Include(p => p.HotelRoom)
                .FirstOrDefault(p => p.HotelRoom.HotelRoomId == model.RoomId);

            if (room == null)
            {

            }



            room.RoomName = model.RoomName;
            room.RoomNo = model.RoomNo;
            room.OcupancyLimit = model.OccupancyLimit;
            room.RsPernight = model.PerNightPrice;

            _context.Update(room);
            _context.SaveChanges();

            if (facilities == null)
            {
                var roomfacility = new RoomFacilities()
                {
                    Internet = model.FreeWifi,
                    AttachedWashRoom = model.AttachedWashroom,
                    Ac = model.Ac,
                    Tv = model.Tv,
                    HotelRoom = _context.hotelRooms.FirstOrDefault(p => p.HotelRoomId == room.HotelRoomId)
                };

                _context.Add(roomfacility);
                _context.SaveChanges();
            }
            else
            {
                facilities.Internet = model.FreeWifi;
                facilities.Tv = model.Tv;
                facilities.Ac = model.Ac;
                facilities.AttachedWashRoom = model.AttachedWashroom;

                _context.Update(facilities);
                _context.SaveChanges();
            }





            return RedirectToAction("Index", new { area = "Manager", controller = "HotelRoomProfile" });
        }
    }
}
