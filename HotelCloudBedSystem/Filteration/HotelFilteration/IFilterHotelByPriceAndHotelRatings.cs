using HotelCloudBedSystem.Models;
using System;
using System.Collections.Generic;

namespace HotelCloudBedSystem.Filteration.HotelFilteration
{
    public interface IFilterHotelByPriceAndHotelRatings
    {
        List<Hotel> GetHotelByPriceAndHotelRatings(string city, int price, int ratingId, DateTime CheckIn, DateTime CheckOut);
    }
}