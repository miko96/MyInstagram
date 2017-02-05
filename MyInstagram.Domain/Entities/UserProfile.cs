using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyInstagram.Domain.Entities
{
    public class UserProfile
    {   
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Country { get; set; }
        public int Phone { get; set; }

        [Key, ForeignKey("AppUser")]
        public string Id { get; set; }
        public ApplicationUser AppUser { get; set; }     
    }
}
