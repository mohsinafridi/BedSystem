using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Models
{
    [Table("CityMapLocation")]
    public class CityMapLocation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityMapLocationId { get; set; }
        public string  CityName { get; set; }
        public string  Latitude { get; set; }
        public string  longtitude { get; set; }

    }
}
