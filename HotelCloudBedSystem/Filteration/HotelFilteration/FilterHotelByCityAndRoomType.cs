using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Filteration.CheckOutCheckIn;
using HotelCloudBedSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelCloudBedSystem.Filteration.HotelFilteration
{
    public class FilterHotelByCityAndRoomType : IFilterHotelByCityAndRoomType
    {
        private HotelCloudDbContext _context;
        private ICheckOutCheckInImplmentation _checkOutCheckInImplmentation;

        public FilterHotelByCityAndRoomType(HotelCloudDbContext context,
            ICheckOutCheckInImplmentation checkOutCheckInImplmentation)
        {
            _context = context;
            _checkOutCheckInImplmentation = checkOutCheckInImplmentation;
        }
        public List<Hotel> GetHotelByCityAndRoomType(string city,
            DateTime checkIn, DateTime CheckOut)
        {
            int NotReservedCount = 0;
            int ReservedCount = 0;
            List<Hotel> HotelList = new List<Hotel>();
            var Hotels = _context.hotels.Where(p => p.HotelCity == city).ToList();

            if (Hotels == null)
            {

            }

            foreach (var hotel in Hotels)
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
                                Check(room.HotelRoomId, checkIn, CheckOut) == true)
                            {
                                ReservedCount++;
                            }
                        }
                    }
                    if (ReservedCount > 0 || NotReservedCount > 0)
                    {
                        HotelList.Add(hotel);
                    }

                    ReservedCount = 0;
                    NotReservedCount = 0;
                }

            }

            return HotelList;


        }
    }
}
