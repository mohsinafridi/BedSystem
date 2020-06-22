using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.ViewModels
{
    public class DashBoardCountViewModel
    {
        public int TotalUserCount { get; set; }
        public int RoleCount { get; set; }
        public int ManagerCount { get; set; }
        public int AdminCount { get; set; }
        public int EndUserCount { get; set; }

        public int EnabledUserCount { get; set; }
        public int DisabledUserCount { get; set; }
        public int NotInRole { get; set; }

        public int InRole { get; set; }
        public int HotelCount { get; set; }
        public int EnabledUserPer { get; set; }
        public int DisableUserPer { get; set; }
        public int NotInROlePer { get; set; }
        public int InRolePer { get; set; }
    }
}
