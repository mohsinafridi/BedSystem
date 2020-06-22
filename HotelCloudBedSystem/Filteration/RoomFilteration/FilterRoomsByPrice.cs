using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Filteration.CheckOutCheckIn;
using HotelCloudBedSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelCloudBedSystem.Filteration.RoomFilteration
{
    public class FilterRoomsByPrice : IFilterRoomsByPrice
    {
        private HotelCloudDbContext _context;
        private ICheckOutCheckInImplmentation _checkOutCheckInImplmentation;

        public FilterRoomsByPrice(HotelCloudDbContext context,
            ICheckOutCheckInImplmentation checkOutCheckInImplmentation)
        {
            _context = context;
            _checkOutCheckInImplmentation = checkOutCheckInImplmentation;
        }


        public List<HotelRoom> GetRoomsByPrice(int hotelId, int roomtypeid,
           int price, DateTime CheckIn, DateTime CheckOut)
        {

            int NotReservedCount = 0;
            int ReservedCount = 0;
            List<HotelRoom> roomList = new List<HotelRoom>();
            var Rooms = _context.hotelRooms.Include(p => p.Hotel).
                Include(p => p.HotelRoomType)
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


                if (ReservedCount > 0 || NotReservedCount > 0)
                {

                    roomList.Add(room);

                }
                ReservedCount = 0;
                NotReservedCount = 0;
            }

            return roomList;
        }
    }
}
