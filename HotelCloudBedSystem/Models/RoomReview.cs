using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Models
{
    [Table("RoomReview")]
    public class RoomReview
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomReviewId { get; set; }
        public int ReviewStar { get; set; }
        public string UserName { get; set; }
        public string Review { get; set; }
        public HotelRoom  hotelRoom { get; set; }
    }
}
