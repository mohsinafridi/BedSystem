using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewModels
{
    public class RoomSearchViewModel
    {
        public int HotelId { get; set; }
        public int hotelRoomTypeId { get; set; }
        public int  Price { get; set; }
        public int StarRatingId { get; set; }
        public List<SelectListItem> Ratings { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
