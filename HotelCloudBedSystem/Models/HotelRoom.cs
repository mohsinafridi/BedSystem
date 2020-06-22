using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Models
{
    [Table("HotelRoom")]
    public class HotelRoom
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelRoomId { get; set; }
        public HotelRoomType   HotelRoomType { get; set; }

        public int RoomNo { get; set; }
        public string  RoomName { get; set; }
        [StringLength(2000)]
        public string  Description { get; set; }
        public HotelFloors   HotelFloors { get; set; }
        public Hotel  Hotel { get; set; }
        public int OcupancyLimit { get; set; }
        public bool IsBooked { get; set; }
        public int  RsPernight { get; set; }
        public byte[] RoomImage { get; set; }

    }
}
