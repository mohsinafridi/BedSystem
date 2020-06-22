using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelCloudBedSystem.Areas.Admin.ViewModels
{
    public class AppRoleCreateViewModel
    {
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        public string  RoleId { get; set; }

        [Display(Name = "Description")]
        [Required]
        [StringLength(500)]
        public string Description { get; set; }


        [Display(Name = "Created")]
        [Required]
        public DateTime Created { get; set; }
    }
}
