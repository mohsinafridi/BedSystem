using HotelCloudBedSystem.Models;
using System;
using System.Collections.Generic;

namespace HotelCloudBedSystem.Filteration.HotelFilteration
{
    public interface IFilterHotelByCityAndRoomType
    {
        List<Hotel> GetHotelByCityAndRoomType(string city, DateTime checkIn, DateTime CheckOut);
    }
}