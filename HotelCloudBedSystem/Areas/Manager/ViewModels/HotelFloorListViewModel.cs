using HotelCloudBedSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.ViewModels
{
    public class HotelFloorListViewModel
    {
        public int HotelFloorsId { get; set; }

        public int FloorNo { get; set; }

        public Hotel Hotel { get; set; }

        public int NoOfRooms { get; set; }

        public string FloorName { get; set; }
    }
}
