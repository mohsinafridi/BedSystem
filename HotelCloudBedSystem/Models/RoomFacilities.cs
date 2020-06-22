using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Models
{
    [Table("RoomFacilities")]
    public class RoomFacilities
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomFacilitiesId { get; set; }

        public bool  Internet { get; set; }
        public bool Tv { get; set; }
        public bool  AttachedWashRoom { get; set; }
        public bool Ac { get; set; }
        public HotelRoom  HotelRoom { get; set; }
    }
}
