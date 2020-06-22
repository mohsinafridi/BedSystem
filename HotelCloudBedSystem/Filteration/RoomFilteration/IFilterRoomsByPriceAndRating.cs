using HotelCloudBedSystem.Models;
using System;
using System.Collections.Generic;

namespace HotelCloudBedSystem.Filteration.RoomFilteration
{
    public interface IFilterRoomsByPriceAndRating
    {
        List<HotelRoom> GetRoomsByPriceAndRating(int hotelId, int roomtypeid, int price, int ratingId, DateTime CheckIn, DateTime CheckOut);
    }
}