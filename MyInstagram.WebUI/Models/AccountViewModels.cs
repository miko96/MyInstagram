using System.ComponentModel.DataAnnotations;

namespace MyInstagram.WebUI.Models
{

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Please enter a first name")]
        [Display(Name = "First name")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Please enter a last name")]
        [Display(Name = "Last name")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a sex")]
        [Display(Name = "Sex")]
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Please enter a country")]
        [Display(Name = "Country")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string Country { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}