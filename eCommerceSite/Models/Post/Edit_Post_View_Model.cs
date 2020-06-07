using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models.Post
{
    [NotMapped]
    public class Edit_Post_View_Model
    {
        [Key]
        public int PID { set; get; }


        [MinLength(20, ErrorMessage = "Title Must be at least 20 character long")]
        [MaxLength(100, ErrorMessage = "Title Can not exceed 100 character")]
        public String Title { set; get; }

        [MinLength(100, ErrorMessage = "Details Must be at least 100 character long")]
        [MaxLength(1000, ErrorMessage = "Details Can not exceed 1000 character")]
        public String Details { set; get; }
    }
}
