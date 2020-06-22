using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.User.ViewModels
{
    public class UserProfilePictureUpdateViewModel
    {
        public byte[] AvatarImages { get; set; }
        public IFormFile avatarImages { get; set; }
    }
}
