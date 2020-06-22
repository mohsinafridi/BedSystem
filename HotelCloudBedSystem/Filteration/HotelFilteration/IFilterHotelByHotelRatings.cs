using HotelCloudBedSystem.Models;
using System;
using System.Collections.Generic;

namespace HotelCloudBedSystem.Filteration.HotelFilteration
{
    public interface IFilterHotelByHotelRatings
    {
        List<Hotel> GetHotelByHotelRatings(string city, int RatingId, DateTime Checkin, DateTime CheckOut);
    }
}