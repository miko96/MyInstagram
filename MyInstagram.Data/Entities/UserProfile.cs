using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyInstagram.Data.Entities
{
    public class UserProfile
    {       
        public string FirstName { get; set; }
        public string LastName { get; set; }      
        public string Sex { get; set; }
        public string Country { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }      
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }     
    }
}
