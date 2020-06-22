using HotelCloudBedSystem.Models;
using System;
using System.Collections.Generic;

namespace HotelCloudBedSystem.Filteration.RoomFilteration
{
    public interface IFilterRoomsByHotelId
    {
        List<HotelRoom> GetRoomsByHotelId(int hotelId, int roomTypeId, DateTime CheckIn, DateTime CheckOut);
    }
}