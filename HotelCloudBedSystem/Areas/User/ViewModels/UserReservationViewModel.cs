using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.User.ViewModels
{
    public class UserReservationViewModel
    {
        public string  UserName { get; set; }
        public string  PhoneNo { get; set; }
        public string  Email { get; set; }
        public string  RoomName { get; set; }
        public int RoomId { get; set; }
        public int RoomNo { get; set; }
        public string  HotelName { get; set; }
        public string  HotelCity { get; set; }
        public int ReservationId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NoOfNight { get; set; }
        public string  UserId { get; set; }

        public int HotelId { get; set; }
        public byte[]  RoomImage { get; set; }
    }
}
