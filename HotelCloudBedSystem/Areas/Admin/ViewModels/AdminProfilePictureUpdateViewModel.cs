using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.ViewModels
{
    public class AdminProfilePictureUpdateViewModel
    {
        public byte[] AvatarImages { get; set; }
        public IFormFile avatarImages { get; set; }
    }
}
