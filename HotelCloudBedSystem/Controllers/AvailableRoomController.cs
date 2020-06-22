using System;
using System.Collections.Generic;
using System.Linq;
using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Filteration.RoomFilteration;
using HotelCloudBedSystem.Models;
using HotelCloudBedSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelCloudBedSystem.Controllers
{
    public class AvailableRoomController : Controller
    {
        private HotelCloudDbContext _context;
        private IEFRepository _repository;
        private IFilterRoomsByPriceAndRating _filterRoomsByPriceAndRating;
        private IFilterRoomsByHotelId _filterRoomsByHotelId;
        private IFilterRoomsByPrice _filterRoomsByPrice;
        private IFilterRoomsByRating _filterRoomsByRating;
        int AverageStar = 0;
        int TotalStar = 0;
        public AvailableRoomController(HotelCloudDbContext context, 
            IEFRepository repository, IFilterRoomsByPriceAndRating 
            filterRoomsByPriceAndRating , IFilterRoomsByHotelId filterRoomsByHotelId,
            IFilterRoomsByPrice filterRoomsByPrice,
            IFilterRoomsByRating filterRoomsByRating)
        {
            _context = context;
            _repository = repository;
            _filterRoomsByPriceAndRating = filterRoomsByPriceAndRating;
            _filterRoomsByHotelId = filterRoomsByHotelId;
            _filterRoomsByPrice = filterRoomsByPrice;
            _filterRoomsByRating = filterRoomsByRating;


        }
        [HttpGet]
        public IActionResult Index(RoomSearchViewModel model)
        {
          
            List<HotelRoom> HotelRooms = new List<HotelRoom>();
            List<AvailbleRoomsViewModel> List = new List<AvailbleRoomsViewModel>();
            AvailbleRoomsViewModel roomModel = null;


            //Filter RoomBy Parice and ReviewStar
            if (model.Price > 0 && model.StarRatingId > 0)
            {
                HotelRooms = _filterRoomsByPriceAndRating.
                    GetRoomsByPriceAndRating(model.HotelId,model.hotelRoomTypeId, 
                    model.Price, model.StarRatingId ,model.CheckInDate ,model.CheckOutDate);
            }

            //Filter Room By HotelId
            else if (model.Price == 0 && model.StarRatingId == 0)
            {
                HotelRooms = _filterRoomsByHotelId.GetRoomsByHotelId(model.HotelId,
                    model.hotelRoomTypeId 
                    ,model.CheckInDate ,model.CheckOutDate);
            }

            //Filter Room By price
            else if (model.Price > 0 && model.StarRatingId == 0)
            {
                HotelRooms = _filterRoomsByPrice.GetRoomsByPrice(model.HotelId,
                    model.hotelRoomTypeId, model.Price, model.CheckInDate, model.CheckOutDate);
            }


            //Filter Room By Review Rating
            else if (model.Price == 0 && model.StarRatingId > 0)
            {
                HotelRooms = _filterRoomsByRating.
                    GetRoomsByRating(model.HotelId, model.hotelRoomTypeId, 
                    model.StarRatingId ,model.CheckInDate ,model.CheckOutDate);
            }


            foreach (var room in HotelRooms)
            {
                var roomreview = _context.roomReviews.Include(p => p.hotelRoom).
                    Where(p => p.hotelRoom.HotelRoomId == room.HotelRoomId).ToList();
                var roomfacilities = _context.RoomFacilities.Include(p => p.HotelRoom).
                    FirstOrDefault(p => p.HotelRoom.HotelRoomId == room.HotelRoomId);
                if (roomreview != null)
                {
                    for (int review = 0; review < roomreview.Count; review++)
                    {
                        TotalStar = TotalStar + roomreview[review].ReviewStar;
                    }

                    AverageStar = TotalStar / roomreview.Count;
                }
                roomModel = new AvailbleRoomsViewModel()
                {
                    RoomId = room.HotelRoomId,
                    RoomNo = room.RoomNo,
                    RoomType = room.HotelRoomType.RoomType,
                    HotelName = room.Hotel.HotelName,
                    HotelId = room.Hotel.HotelId,
                    RoomName = room.RoomName,
                    City = room.Hotel.HotelCity,
                    RsperNight = room.RsPernight,
                    RoomImage = room.RoomImage,
                    AverageStar = AverageStar,
                    totalReview= roomreview.Count,
                    hotelRoomTypeid=room.HotelRoomType.HotelRoomTypeId,
                    CheckIn=model.CheckInDate,
                    CheckOut=model.CheckOutDate

                };

              

                List.Add(roomModel);
                TotalStar = 0;
                AverageStar = 0;
            }



            return View(List);
            
        }

        
    }
}