using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Models
{
    public class AppRole:IdentityRole
    {
        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime Created { get; set; }
    }
}
