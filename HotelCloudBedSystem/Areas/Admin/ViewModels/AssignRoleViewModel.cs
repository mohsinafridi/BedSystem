using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.ViewModels
{
    public class AssignRoleViewModel
    {
        public string AppUserRoleId { get; set; }
        public string AppRoleId { get; set; }
        public string Name { get; set; }
        public bool IsAssigned { get; set; }
        public Nullable<DateTime> Created { get; set; }
    }
}
