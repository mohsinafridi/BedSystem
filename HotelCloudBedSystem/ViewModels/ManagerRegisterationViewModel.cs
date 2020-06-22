using System.ComponentModel.DataAnnotations;

namespace HotelCloudBedSystem.ViewModels
{
    public class ManagerRegisterationViewModel
    {
     
        public string FirstName { get; set; }

        
        public string LastName { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string  PhoneNo { get; set; }

        public string  Address { get; set; }

        [StringLength(2000)]
        public string Aboutyou { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
