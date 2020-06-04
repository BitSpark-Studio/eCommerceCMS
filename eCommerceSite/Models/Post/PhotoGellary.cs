using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models.Post
{
    public class PhotoGellary
    {
        [Key]
        public int ID { set; get; }

        [ForeignKey("Post")]
        public int PID { set; get; }

        public Post Post { set; get; }

        [MaxLength(200)]
        [Required]
        public String Picture_Name { set; get; }
    }
}
