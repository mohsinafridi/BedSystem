using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.ViewModels
{
    public class HotelSearchedByNameViewModel
    {
        public HotelSearchedByNameViewModel()
        {
            Hotel = new List<SelectListItem>();
        }
        public List<SelectListItem> Hotel { get; set; }

        public int Hotelid { get; set; }

    }
}
