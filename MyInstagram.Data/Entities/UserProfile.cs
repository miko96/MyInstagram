using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyInstagram.Data.Entities
{
    public class UserProfile
    {
        [Required(ErrorMessage = "Please enter a first name")]
        [Display(Name = "First name")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Please enter a last name")]
        [Display(Name = "Last name")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        
        public string Sex { get; set; }

        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Country { get; set; }

        //[DataType(DataType.PhoneNumber)]

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
       
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }     
    }
}
