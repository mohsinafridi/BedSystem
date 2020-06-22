using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.User.ViewModels
{
    public class AddReviewViewModel
    {
        public int ReviewId { get; set; }
        public string  UserName { get; set; }
        public int ReviewStar { get; set; }
        public string  Review { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public int StarRateValuew { get; set; }
        public string SatarName { get; set; }
        public List<SelectListItem> Ratings { get; set; }
    }
}
