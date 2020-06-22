using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.ViewModels
{
    public class HotelListViewModel
    {
        public int HotelId { get; set; }

        public string HotelName { get; set; }

        public int NoOfFloors { get; set; }

        public int NoOfRooms { get; set; }
        public string HotelCity { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string  Manager { get; set; }
        public byte[] Hotelimage { get; set; }
    }
}
