using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.ViewModels
{
    public class ManagerDashboardCountViewModel
    {
        public int hotelTotalRooms { get; set; }
        public int floorTotalRooms { get; set; }
        public int HotelBookedRooms { get; set; }
        public int FloorBookedRooms { get; set; }
        public int HotelNotBookedRooms { get; set; }
        public int FloorNotBookedRooms { get; set; }
        public int TotalFloors { get; set; }
        public int HotelPaymentCount { get; set; }
        public int floorPaymentCount { get; set; }
        public int roomPaymentCount { get; set; }
        public int HotelTotalReservation { get; set; }
        public int RoomTotalReservation { get; set; }
    }
}
