using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Models
{
    [Table("RoomsImage")]
    public class RoomsImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomsImageId { get; set; }

        public HotelRoom  HotelRoom { get; set; }

      
        public byte[] images { get; set; }
    }
}
