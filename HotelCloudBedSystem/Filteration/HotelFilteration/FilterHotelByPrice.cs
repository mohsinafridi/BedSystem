using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Filteration.CheckOutCheckIn;
using HotelCloudBedSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelCloudBedSystem.Filteration.HotelFilteration
{
    public class FilterHotelByPrice : IFilterHotelByPrice
    {
        private HotelCloudDbContext _context;
        private ICheckOutCheckInImplmentation _checkOutCheckInImplmentation;
        int Averageprice = 0;
        int TotalPrice = 0;
        public FilterHotelByPrice(HotelCloudDbContext context,
            ICheckOutCheckInImplmentation checkOutCheckInImplmentation)
        {
            _context = context;
            _checkOutCheckInImplmentation = checkOutCheckInImplmentation;
        }
        public List<Hotel> GetHotelByPrice(string city, int Price,
            DateTime CheckIn, DateTime CheckOut)
        {
            int ReservedCount = 0;
            int NotReservedCount = 0;
            List<Hotel> HotelList = new List<Hotel>();
            var hotels = _context.hotels
                  .Where(p => p.HotelCity == city).ToList();

            foreach (var hotel in hotels)
            {
                var Rooms = _context.hotelRooms.
                    Include(p => p.Hotel).Where(p => p.Hotel.HotelId ==
                    hotel.HotelId).ToList();

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


                if (NotReservedCount > 0 || ReservedCount > 0)
                {
                    if (Averageprice == Price)
                    {
                        HotelList.Add(hotel);
                    }
                }


                Averageprice = 0;
                TotalPrice = 0;
                NotReservedCount = 0;
                ReservedCount = 0;

            }

            return HotelList;
        }

    }
}
