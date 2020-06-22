using HotelCloudBedSystem.Data;
using HotelCloudBedSystem.Filteration.CheckOutCheckIn;
using HotelCloudBedSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelCloudBedSystem.Filteration.RoomFilteration
{
    public class FilterRoomsByHotelId : IFilterRoomsByHotelId
    {
        private HotelCloudDbContext _context;
        private ICheckOutCheckInImplmentation _checkOutCheckInImplmentation;
        public FilterRoomsByHotelId(HotelCloudDbContext context ,
            ICheckOutCheckInImplmentation checkOutCheckInImplmentation)
        {
            _context = context;
            _checkOutCheckInImplmentation = checkOutCheckInImplmentation;
        }


        public List<HotelRoom> GetRoomsByHotelId(int hotelId, int roomTypeId,
            DateTime CheckIn, DateTime CheckOut)
        {
            int NotReservedCount = 0;
            int ReservedCount = 0;
            List<HotelRoom> roomList = new List<HotelRoom>();
            var HotelRooms = _context.hotelRooms.Include(p => p.Hotel).Include(p => p.HotelRoomType)
                .Where(p => p.Hotel.HotelId == hotelId &&
                p.HotelRoomType.HotelRoomTypeId == roomTypeId).ToList();

            if (HotelRooms == null)
            {

            }

            foreach (var room in HotelRooms)
            {
                if (room.IsBooked == false)
                {
                    NotReservedCount++;
                }
                else if (room.IsBooked == true)
                {
                    
                    if(_checkOutCheckInImplmentation.
                        Check(room.HotelRoomId , CheckIn, CheckOut) == true)
                    {
                        ReservedCount++;
                    }

                }

                if (NotReservedCount > 0 || ReservedCount > 0)
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
