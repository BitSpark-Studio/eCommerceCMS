using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models.Post
{
    public class Post
    {
        [Key]
        public int PID { set; get; }

        public List<PhotoGellary> Photos { set; get; }

        [MinLength(20, ErrorMessage = "Title Must be at least 20 character long")]
        [MaxLength(100, ErrorMessage = "Title Can not exceed 100 character")]
        public String Title { set; get; }

        [MinLength(100, ErrorMessage = "Details Must be at least 100 character long")]
        [MaxLength(1000, ErrorMessage = "Details Can not exceed 1000 character")]
        public String Details { set; get; }

        [MaxLength(4)]
        public String Approve { set; get; }

        public List<Joining.Cetagory_Owner_Post> Cetagory_Owner_Post { set; get; }
    }
}
