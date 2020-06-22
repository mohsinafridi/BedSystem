using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.ViewModels
{
    public class AddHotelViewModel
    {
        public string HotelName { get; set; }

        public int NoOfFloors { get; set; }

        public string  HotelCity { get; set; }
        public int NoOfRooms { get; set; }
        //public AppUser AppUser { get; set; }
        //public string Description { get; set; }

        public string  AppUserId { get; set; }
        public List<SelectListItem> AppUser { get; set; }


    }
}
