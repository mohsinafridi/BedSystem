using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Filteration.CheckOutCheckIn;
using HotelCloudBedSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelCloudBedSystem.Filteration.HotelFilteration
{
    public class FilterHotelByHotelRatings : IFilterHotelByHotelRatings
    {
        private HotelCloudDbContext _context;
        private ICheckOutCheckInImplmentation _checkOutCheckInImplmentation;
        int AverageStar = 0;
        int TotalStar = 0;
        public FilterHotelByHotelRatings(HotelCloudDbContext context,
             ICheckOutCheckInImplmentation checkOutCheckInImplmentation)
        {
            _context = context;
            _checkOutCheckInImplmentation = checkOutCheckInImplmentation;
        }
        public List<Hotel> GetHotelByHotelRatings(string city, int RatingId,
            DateTime Checkin, DateTime CheckOut)
        {
            int NotReservedCount = 0;
            int ReservedCount = 0;
            List<Hotel> HotelList = new List<Hotel>();
            var hotels = _context.hotels
                  .Where(p => p.HotelCity == city).ToList();

            foreach (var hotel in hotels)
            {
                var hotelRooms = _context.hotelRooms.
                    Include(p => p.Hotel).
                    Where(p => p.Hotel.HotelId == hotel.HotelId).ToList();

                if (hotelRooms != null)
                {
                    foreach (var room in hotelRooms)
                    {
                        if (room.IsBooked == false)
                        {
                            NotReservedCount++;
                        }
                        else if (room.IsBooked == true)
                        {
                            if (_checkOutCheckInImplmentation.
                                Check(room.HotelRoomId, Checkin, CheckOut) == true)
                            {
                                ReservedCount++;
                            }
                        }
                    }


                }

                var Averagereview = _context.hotelReviews.
                  Include(p => p.hotel).Where(p => p.hotel.HotelId == hotel.HotelId).ToList();

                if (Averagereview != null)
                {
                    for (int reviewloop = 0; reviewloop < Averagereview.Count; reviewloop++)
                    {
                        TotalStar = TotalStar + Averagereview[reviewloop].ReviewStar;
                    }

                    AverageStar = TotalStar / Averagereview.Count;
                }
                var Star = _context.starRatings.FirstOrDefault(p => p.StarRatingId == RatingId);

                if (ReservedCount > 0 || NotReservedCount > 0)
                {
                    if (AverageStar == Star.StarNo)
                    {
                        HotelList.Add(hotel);
                    }
                }


                AverageStar = 0;
                TotalStar = 0;
                ReservedCount = 0;
                NotReservedCount = 0;


            }
            return HotelList;
        }
    }
}
