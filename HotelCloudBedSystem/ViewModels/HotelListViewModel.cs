using System;

namespace HotelCloudBedSystem.ViewModels
{
    public class HotelListViewModel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelCity { get; set; }
        public int NoOfRoomsFree { get; set; }
        public byte[] HotelImage { get; set; }
        public string  RoomType { get; set; }
        public int NoOfRooms { get; set; }
        public int ReviewStar { get; set; }
        public int AveragePrice { get; set; }
        public int TotalReview { get; set; }
        public string  hotelRoomtype { get; set; }
        public int HotelRoomTypeId { get; set; }
        public DateTime   CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
