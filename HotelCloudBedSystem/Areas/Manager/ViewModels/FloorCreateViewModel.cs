using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.ViewModels
{
    public class FloorCreateViewModel
    {
        public int HotelFloorsId { get; set; }

        public int FloorNo { get; set; }

        public Hotel Hotel { get; set; }

        public int NoOfRooms { get; set; }

        public string FloorName { get; set; }
        public int HotelId { get; set; }
        public string  HotelName { get; set; }
        public List<SelectListItem> Hotels { get; set; }
    }
}
