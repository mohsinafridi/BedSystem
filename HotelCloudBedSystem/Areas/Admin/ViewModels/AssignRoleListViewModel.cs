using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.ViewModels
{
    public class AssignRoleListViewModel
    {
        public AssignRoleListViewModel()
        {
            AssignRole = new List<AssignRoleViewModel>();
        }
        public string AppUserId { get; set; }
        public static string StaticUserId { get; set; }

        public List<AssignRoleViewModel> AssignRole { get; set; }
    }
}
