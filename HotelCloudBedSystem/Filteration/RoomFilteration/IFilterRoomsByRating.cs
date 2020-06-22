using HotelCloudBedSystem.Models;
using System;
using System.Collections.Generic;

namespace HotelCloudBedSystem.Filteration.RoomFilteration
{
    public interface IFilterRoomsByRating
    {
        List<HotelRoom> GetRoomsByRating(int hotelId, int roomtypeid, int ratingId, DateTime CheckIn, DateTime CheckOut);
    }
}