using HotelCloudBedSystem.Models;
using System;
using System.Collections.Generic;

namespace HotelCloudBedSystem.Filteration.HotelFilteration
{
    public interface IHotelNetworkMainSearchEngine
    {
        List<Hotel> GetHotelBySearchEngine(string city, DateTime chkin, DateTime chkout);
    }
}