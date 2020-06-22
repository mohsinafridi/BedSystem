using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Models
{
    [Table("TemporaryRoomType")]
    public class TemporaryRoomType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TemporaryRoomTypeId { get; set; }

        public HotelRoomType  HotelRoomType { get; set; }
        public Hotel  hotel { get; set; }
    }
}
