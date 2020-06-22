using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Manager.ViewModels
{
    public class ManagerProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Nationality { get; set; }
        public DateTime Created { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte[] AvatarImage { get; set; }
        public string role { get; set; }
        public IFormFile avatarimage { get; set; }
        public string specialization { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsEnabled { get; set; }
        public string Address { get; set; }
        public string  AboutYou { get; set; }
    }
}
