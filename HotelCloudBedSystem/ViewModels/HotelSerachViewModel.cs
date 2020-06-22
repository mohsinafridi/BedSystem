using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewModels
{
    public class HotelSerachViewModel
    {
        public string City { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomType { get; set; }
        public List<SelectListItem> Roomtypes { get; set; }
        public int Price { get; set; }
        public int StarRatingid { get; set; }
        public string Satarname { get; set; }
        public List<SelectListItem> Ratings { get; set; }
        public int hotelRoomTypeId { get; set; }

        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }

    }
}
