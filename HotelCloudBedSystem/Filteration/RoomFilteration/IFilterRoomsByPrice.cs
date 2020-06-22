using HotelCloudBedSystem.Models;
using System;
using System.Collections.Generic;

namespace HotelCloudBedSystem.Filteration.RoomFilteration
{
    public interface IFilterRoomsByPrice
    {
        List<HotelRoom> GetRoomsByPrice(int hotelId, int roomtypeid, int price, DateTime CheckIn, DateTime CheckOut);
    }
}