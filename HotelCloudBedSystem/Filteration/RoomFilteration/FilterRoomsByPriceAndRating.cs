using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Filteration.CheckOutCheckIn;
using HotelCloudBedSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelCloudBedSystem.Filteration.RoomFilteration
{
    public class FilterRoomsByPriceAndRating : IFilterRoomsByPriceAndRating
    {
        private HotelCloudDbContext _context;
        int AverageStar = 0;
        int TotalStar = 0;
        private ICheckOutCheckInImplmentation _checkOutCheckInImplmentation;
        public FilterRoomsByPriceAndRating(HotelCloudDbContext context,
            ICheckOutCheckInImplmentation checkOutCheckInImplmentation)
        {
            _context = context;
            _checkOutCheckInImplmentation = checkOutCheckInImplmentation;
        }

        public List<HotelRoom> GetRoomsByPriceAndRating(int hotelId, int roomtypeid,
            int price, int ratingId, DateTime CheckIn, DateTime CheckOut)
        {

            int NotReservedCount = 0;
            int ReservedCount = 0;
            List<HotelRoom> roomList = new List<HotelRoom>();
            var Rooms = _context.hotelRooms.Include(p => p.Hotel).Include(p => p.HotelRoomType)
                .Where(p => p.Hotel.HotelId == hotelId &&
                p.HotelRoomType.HotelRoomTypeId == roomtypeid &&
                p.RsPernight == price).ToList();

            foreach (var room in Rooms)
            {

                if (room.IsBooked == false)
                {
                    NotReservedCount++;
                }
                else if (room.IsBooked == true)
                {
                    if (_checkOutCheckInImplmentation.
                        Check(room.HotelRoomId, CheckIn, CheckOut) == true)
                    {
                        ReservedCount++;
                    }
                }

                var Averagereview = _context.roomReviews.
                  Include(p => p.hotelRoom).Where(p => p.hotelRoom.HotelRoomId == room.HotelRoomId).ToList();

                if (Averagereview != null)
                {
                    for (int reviewloop = 0; reviewloop < Averagereview.Count; reviewloop++)
                    {
                        TotalStar = TotalStar + Averagereview[reviewloop].ReviewStar;
                    }

                    AverageStar = TotalStar / Averagereview.Count;
                }

                var Star = _context.starRatings.FirstOrDefault(p => p.StarRatingId == ratingId);

                if (ReservedCount > 0 || NotReservedCount > 0)
                {
                    if (AverageStar == Star.StarNo)
                    {
                        roomList.Add(room);
                    }
                }

                TotalStar = 0;
                AverageStar = 0;
                ReservedCount = 0;
                NotReservedCount = 0;
            }

            return roomList;
        }
    }
}
