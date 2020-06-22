using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.ViewModels
{
    public class HotelImagesViewModel
    {
        public byte[] Hotelimage { get; set; }
        public IFormFile hotelimage { get; set; }
        public int HotelId { get; set; }
    }
}
