using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Models
{
    [Table("StarRating")]
    public class StarRating
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StarRatingId { get; set; }
        public int StarNo { get; set; }
        public string  StarName { get; set; }

    }
}