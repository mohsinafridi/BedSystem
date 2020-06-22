using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Models
{
    [Table("HotelFloors")]
    public class HotelFloors
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelFloorsId { get; set; }

        public int FloorNo { get; set; }

        public Hotel  Hotel { get; set; }

        public int NoOfRooms { get; set; }

        public string  FloorName { get; set; }
    }
}
