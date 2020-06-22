using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewModels
{
    public class RoomFilteringViewModel
    {
        public int Price { get; set; }
        public int StarRateValuew { get; set; }
        public string  SatarName { get; set; }
        public List<SelectListItem> Ratings { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }


    }
}
