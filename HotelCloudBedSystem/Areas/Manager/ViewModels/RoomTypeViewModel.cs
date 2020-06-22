using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.ViewModels
{
    public class RoomTypeViewModel
    {
        public int RoomTypeId { get; set; }
        public string  RoomType { get; set; }
        public List<SelectListItem> Roomtypes { get; set; }
    }
}
