using HotelCloudBedSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.ViewModels
{
    public class RoomCreateViewModel
    {
        public int RoomId { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public HotelFloors HotelFloors { get; set; }
        public Hotel Hotel { get; set; }
        public int OcupancyLimit { get; set; }
        public bool IsBooked { get; set; }

        public int HotelId { get; set; }
        public List<SelectListItem> Hotels { get; set; }

        public string  HotelName { get; set; }

        public int MyProperty { get; set; }
        public string  FloorName { get; set; }
        public int FloorId { get; set; }
        public int RoomNo { get; set; }
        public string  RoomName { get; set; }
        public List<SelectListItem> Floors { get; set; }

        public int RoomTypeId { get; set; }
        public string RoomType { get; set; }
        public List<SelectListItem> Roomtypes { get; set; }
    }
}
