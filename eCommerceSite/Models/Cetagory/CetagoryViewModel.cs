using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models.Cetagory
{
    [NotMapped]
    public class CetagoryViewModel
    {

        [Key]

        public int CID { set; get; }
 
        [NotMapped]
        [Required]
        [Display(Name="Upload Cetagory Picture")]
        public IFormFile Photo { set; get; }
        [Required]
       //// [MinLength(20, ErrorMessage = "Title Must be at least 20 character long")]
        [MaxLength(100, ErrorMessage = "Title Can be larger then 100 Character")]
        [Display(Name = "Cetagory Name")]
        [Remote(action: "Same_Cetagory_Name", controller: "Admin", ErrorMessage = "This Title Already Exsist")]
        public String Title { set; get; }

        [Required]
      ////  [MinLength(100, ErrorMessage = "Details Must be at least 100 character long")]
        [MaxLength(1000, ErrorMessage = "Details Can not be larger than 1000 Character")]
        public String Details { set; get; }
    }
}
