using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Models
{
    public class AdminPermission
    {
        public int AdminPermissionId { get; set; }
        public string AdminName { get; set; }
        public string  Description { get; set; }
        public bool IsPermited { get; set; }
        public DateTime  CreatedDate { get; set; }
        public DateTime PermitedDate { get; set; }
    }
}
