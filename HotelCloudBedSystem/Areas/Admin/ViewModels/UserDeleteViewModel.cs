using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.ViewModels
{
    public class UserDeleteViewModel
    {

        public UserDeleteViewModel()
        {
            RolesList = new List<string>();
        }

        public string  UserId { get; set; }
        public string  FirstName { get; set; }
        public string  LastName { get; set; }
        public string  Email { get; set; }
        public string  PhoneNo { get; set; }
        public string  Role { get; set; }

        public List<string> RolesList { get; set; }


    }
}
