using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Filteration.CheckOutCheckIn;
using HotelCloudBedSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelCloudBedSystem.Filteration.HotelFilteration
{
    public class FilterHotelByPriceAndHotelRatings : IFilterHotelByPriceAndHotelRatings
    {
        private HotelCloudDbContext _context;
        private ICheckOutCheckInImplmentation _checkOutCheckInImplmentation;

        public FilterHotelByPriceAndHotelRatings(HotelCloudDbContext context,
            ICheckOutCheckInImplmentation checkOutCheckInImplmentation)
        {
            _context = context;
            _checkOutCheckInImplmentation = checkOutCheckInImplmentation;
        }
        public List<Hotel> GetHotelByPriceAndHotelRatings(string city, int price,
            int ratingId, DateTime CheckIn, DateTime CheckOut)
        {
            int ReservedCount = 0, NotReservedCount = 0, AverageStar = 0;
            int Averageprice = 0, TotalPrice = 0, TotalStar = 0;
            List<Hotel> HotelList = new List<Hotel>();
            var hotels = _context.hotels
                  .Where(p => p.HotelCity == city).ToList();

            foreach (var hotel in hotels)
            {
                var Rooms = _context.hotelRooms.
                    Include(p => p.Hotel).Where(p => p.Hotel.HotelId == hotel.HotelId).ToList();

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
                }


                if (Rooms != null)
                {
                    for (int priceloop = 0; priceloop < Rooms.Count; priceloop++)
                    {
                        TotalPrice = TotalPrice + Rooms[priceloop].RsPernight;
                    }

                    Averageprice = TotalPrice / (Rooms.Count);
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
                var Star = _context.starRatings.FirstOrDefault(p => p.StarRatingId == ratingId);

                if (NotReservedCount > 0 || ReservedCount > 0)
                {
                    if (Averageprice == price && AverageStar == Star.StarNo)
                    {
                        HotelList.Add(hotel);
                    }

                }

                AverageStar = 0;
                Averageprice = 0;
                TotalPrice = 0;
                TotalStar = 0;
                NotReservedCount = 0;
                ReservedCount = 0;


            }
            return HotelList;
        }


    }
}
