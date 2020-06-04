using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models.Cetagory
{
    public class Cetagory
    {

        [Key]

        public int CID { set; get; }
        [MaxLength(100)]

        [Display(Name="Cetagory Picture")]
        public String Photo { set; get; }
        [MinLength(20, ErrorMessage = "Title Must be at least 20 character long")]
        [MaxLength(100,ErrorMessage ="Title Can be larger then 100 Character")]
        [Display(Name="Cetagory Name")]
        [Remote(controller:"Admin", action: "Same_Cetagory_Name", ErrorMessage ="This Title Already Exsist")]
        public String Title { set; get; }

        [MinLength(100, ErrorMessage = "Details Must be at least 100 character long")]
        [MaxLength(1000,ErrorMessage ="Details Can not be larger than 1000 Character")]
        public String Details { set; get; }

        public List<Joining.Cetagory_Owner_Post> Cetagory_Owner_Post { set; get; }

    }
}
