using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Models
{
    [Table("HotelImages")]
    public class HotelImages
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelImagesId { get; set; }
        public byte[] HImage { get; set; }
     
        public Hotel Hotel { get; set; }


    }
}
