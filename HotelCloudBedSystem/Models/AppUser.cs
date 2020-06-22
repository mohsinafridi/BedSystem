using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace HotelCloudBedSystem.Models
{
    public class AppUser:IdentityUser
    {
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }


        public DateTime Created { get; set; }


        public DateTime DateOfBirth { get; set; }


        public byte[] AvatarImage { get; set; }

        [StringLength(2000)]
        public string  Aboutyou { get; set; }

        public string  Address { get; set; }

        public AppRole  AppRole { get; set; }

        public bool IsEnable { get; set; }
        public bool AccountStatus { get; internal set; }

        public bool RequestAccept { get; set; }
    }
}
