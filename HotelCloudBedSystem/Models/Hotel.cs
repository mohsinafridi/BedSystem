using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Models
{
    [Table("Hotel")]
    public class Hotel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelId { get; set; }

        public string HotelName { get; set; }

        public int NoOfFloors { get; set; }

        public int NoOfRooms { get; set; }
        public string  AboutHotel { get; set; }
        public string  HotelCity { get; set; }
        
        public int ZipCode { get; set; }
        public string  Address { get; set; }
        public AppUser  AppUser { get; set; }
        public string  Description { get; set; }
        public byte[] HotelImage { get; set; }


    }
}
