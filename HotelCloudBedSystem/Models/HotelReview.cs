using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Models
{
    [Table("HotelReview")]
    public class HotelReview
    {
        public int HotelReviewId { get; set; }
        public int ReviewStar { get; set; }
        public string  UserName { get; set; }
        public string  Review { get; set; }
        public Hotel hotel { get; set; }
    }
}
