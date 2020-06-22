using HotelCloudBedSystem.Models;
using System;
using System.Collections.Generic;

namespace HotelCloudBedSystem.Filteration.HotelFilteration
{
    public interface IFilterHotelByPrice
    {
        List<Hotel> GetHotelByPrice(string city, int Price, DateTime CheckIn, DateTime CheckOut);
    }
}