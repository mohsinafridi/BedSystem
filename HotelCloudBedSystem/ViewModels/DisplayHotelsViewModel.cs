using HotelCloudBedSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewModels
{
    public class DisplayHotelsViewModel
    {
        public string  HotelName { get; set; }
        public string   HotelCity { get; set; }
        public int AvailableRooms  { get; set; }
        public byte[] Image { get; set; }

    }
}
