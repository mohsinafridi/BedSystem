using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewModels
{
    public class AvailbleRoomsViewModel
    {
        public int  RoomId { get; set; }
        public int  RoomNo { get; set; }
        public string  RoomType { get; set; }
        public string  HotelName { get; set; }
        public int HotelId { get; set; }
        public string  RoomName { get; set; }
        public int RsperNight { get; set; }
        public string  City { get; set; }
        public byte[] RoomImage { get; set; }
        public int AverageStar { get; set; }
        public int totalReview { get; set; }
        public int hotelRoomTypeid { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

    }
}
