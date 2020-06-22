using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.ViewModels
{
    public class HotelRoomProfilePictureViewModel
    {
        public byte[] RoomImages { get; set; }
        public IFormFile roomImages { get; set; }
        public int   RoomId  { get; set; }
    }
}
