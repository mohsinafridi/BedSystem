using HotelCloudBedSystem.Areas.Manager.ViewModels;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace HotelCloudBedSystem.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class HotelRoomsController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IEFRepository _repository;
        private HotelCloudDbContext _context;

        public HotelRoomsController(UserManager<AppUser> userManager,
            IEFRepository repository,
            HotelCloudDbContext context)
        {
            _userManager = userManager;
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(QueryOptions options)
        {

            var hotel = FindHotel();
            if (hotel != null)
            {

                return View(_repository.RoomsByHotelId(options, hotel.HotelId, 0, 0, true));

            }

            return View();
        }

        [HttpGet]
        public IActionResult BookedRooms(QueryOptions options)
        {
            var hotel = FindHotel();
            if (hotel != null)
            {


                var rooms = _repository.BookedRoomsByHotelId(options, hotel.HotelId, 0, 0, true);
                return View(rooms);
            }

            return View();


        }

        [HttpGet]
        public IActionResult UnBookedRooms(QueryOptions options)
        {
            var hotel = FindHotel();
            if (hotel != null)
            {

                return View(_repository.UnBookedRoomsByHotelId(options, hotel.HotelId, 0, 0, true));

            }

            return View();
        }


        #region FindHotel
        public Hotel FindHotel()
        {
            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;

            if (OnlineUser == null && !(_userManager.IsInRoleAsync(OnlineUser, "Manager").Result))
            {

            }
            return _repository.GetHotelByManagerId(OnlineUser.Id, true);
        }

        #endregion

        [HttpGet]
        public IActionResult Create()
        {


            RoomCreateViewModel model = null;

            var OnlineUser = _userManager.GetUserAsync(HttpContext.User).Result;

            if (OnlineUser == null && !(_userManager.IsInRoleAsync(OnlineUser, "Manager").Result))
            {

            }
            var hotel = _repository.GetHotelByManagerId(OnlineUser.Id, true);
            //var hotel = _repository.GetHotels();
            var floors = _repository.getHotelFloorsbyHotelId(hotel.HotelId);

            if (hotel != null)
            {
                model = new RoomCreateViewModel()
                {
                    HotelId = hotel.HotelId,
                    HotelName = hotel.HotelName,
                    Floors = floors.Select(p => new SelectListItem()
                    {
                        Text = p.FloorName,
                        Value = p.HotelFloorsId.ToString()
                    }).ToList(),
                };
            }
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Create(RoomCreateViewModel model)
        {
            bool Status = false;
            string Message = string.Empty;
            if (ModelState.IsValid)
            {
                var hotel = new HotelRoom()
                {
                    //RoomType = model.RoomType,
                    RoomName=model.RoomName,
                    RoomNo=model.RoomNo,
                    HotelFloors = model.HotelFloors,
                    Description = model.Description,
                    IsBooked = false,
                    Hotel = _repository.HotelById(model.HotelId),
                    HotelRoomType = _context.hotelRoomTypes
                    .FirstOrDefault(p => p.HotelRoomTypeId == model.RoomTypeId)
                };

                _repository.Add(hotel);

                if (_repository.SaveChange())
                {

                    return RedirectToAction("Index", new { area = "Manager", controller = "HotelRooms" });
                }
                else
                {
                    Status = false;
                    Message = "Error inserting /Creating Course";
                }
            }
            else
            {
                ModelState.AddModelError("", "invalid / incomplete data");
            }
            //return Json(new { status = Status, message = Message });
            return View();
        }


        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            int count = 0;

            RoomCreateViewModel model = null;
            //var hotel = _repository.GetHotels();
            //var floors = _repository.GetFloors();
            //var hotel = _repository.GetHotels();
            var room = _repository.RoomById(id);
            if (room != null)
            {
                model = new RoomCreateViewModel()
                {
                    RoomId = room.HotelRoomId,
                    Description = room.Description,
                    OcupancyLimit = room.OcupancyLimit,
                    RoomName = room.RoomName,
                    RoomNo = room.RoomNo,
                    RoomType = room.HotelRoomType.RoomType



                };
            }

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Update(RoomCreateViewModel model)
        {
            bool Status = false;
            string Message = string.Empty;
            if (ModelState.IsValid)
            {
                var hotel = new HotelRoom()
                {
                    //RoomType = model.RoomType,
                    OcupancyLimit = model.OcupancyLimit,
                    HotelFloors = model.HotelFloors,
                    Description = model.Description,
                    IsBooked = false,
                    Hotel = _repository.HotelById(model.HotelId),
                    HotelRoomType=_context.hotelRoomTypes.
                    FirstOrDefault(p=> p.HotelRoomTypeId ==model.RoomTypeId),
                    RoomName=model.RoomName,
                    RoomNo=model.RoomNo
                    

                };

                _repository.Update(hotel);

                if (_repository.SaveChange())
                {

                    return RedirectToAction("Index", new { area = "Manager", controller = "HotelRooms" });
                }
                else
                {
                    Status = false;
                    Message = "Error inserting /Creating Course";
                }
            }
            else
            {
                ModelState.AddModelError("", "invalid / incomplete data");
            }
            //return Json(new { status = Status, message = Message });
            return View();
        }

        #endregion

        #region Delete

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var room = _repository.RoomById(id);

            if (room != null)
            {
                _repository.Delete(room);

                if (_repository.SaveChange())
                {
                    return RedirectToAction("Index", new { area = "Manager", controller = "HotelRooms" });
                }
            }

            return View();
        }
        #endregion
    }
}
