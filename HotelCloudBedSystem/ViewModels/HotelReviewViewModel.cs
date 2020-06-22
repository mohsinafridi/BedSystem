using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.ViewModels
{
    public class HotelReviewViewModel
    {
        public string  UserName { get; set; }
        public int ReviewStar { get; set; }
        public string  Review { get; set; }
    }
}
