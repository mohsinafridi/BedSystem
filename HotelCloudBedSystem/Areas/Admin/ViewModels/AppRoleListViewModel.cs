using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.ViewModels
{
    public class AppRoleListViewModel
    {

        public string AppRoleId { get; set; }
        public string RoleName { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }
    }
}
