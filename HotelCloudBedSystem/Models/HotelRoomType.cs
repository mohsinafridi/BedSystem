using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Models
{
    [Table("HotelRoomType")]
    public class HotelRoomType
    {
        public int HotelRoomTypeId { get; set; }
        public string  RoomType { get; set; }

    }
}
