using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewModels
{
    public class HotelDetailViewModel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelCity { get; set; }
        public string  Description { get; set; }
        public string  Address { get; set; }
        public int NoOfRooms { get; set; }
        public int FreeRoomsCount { get; set; }
        public int bookedRoomCount { get; set; }
        public byte[] HotelImage { get; set; }
        public int ReviewStar { get; set; }
        public int AveragePrice { get; set; }
        public int TotalReview { get; set; }
        public bool FreeWifi { get; set; }
        public bool BreckFast { get; set; }
        public bool Lunch { get; set; }
        public bool AttachedWashrooms { get; set; }
        public bool Receptionservices { get; set; }
        public bool Dinner { get; set; }
        public bool CarParking { get; set; }
        public bool Laundry { get; set; }
    }
}
