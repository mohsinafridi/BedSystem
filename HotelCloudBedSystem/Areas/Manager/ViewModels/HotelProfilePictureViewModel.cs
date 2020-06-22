using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.ViewModels
{
    public class HotelProfilePictureViewModel
    {
        public byte[] AvatarImages { get; set; }
        public IFormFile avatarImages { get; set; }
    }
}
