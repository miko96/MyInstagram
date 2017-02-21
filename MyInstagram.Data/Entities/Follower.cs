using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInstagram.Data.Entities
{
    public class Follow
    {
        [Key]
        [Column(Order = 0)]
        public string FromUserID { get; set; }
        [ForeignKey("FromUserID")]
        public ApplicationUser FromUser { get; set; }


        [Column(Order = 1)]
        public string ToUserID { get; set; }
        [ForeignKey("ToUserID")]
        public ApplicationUser ToUser { get; set; }

        public int FollowStatus { get; set; }
    }
}
