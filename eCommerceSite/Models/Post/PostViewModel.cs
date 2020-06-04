using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models.Post
{
    [NotMapped]
    public class PostViewModel
    {
        [Key]
        public int PID { set; get; }

        [ForeignKey("Cetagory")]
        public int CID { set; get; }
        public Cetagory.Cetagory cetagory { set; get; }
        [ForeignKey("Owner")]
        public int OID { set; get; }
        public Owner.Owner owner { set; get; }
        [Required]
        [Display(Name="Product Image 1")]
        public IFormFile Photo_1 { set; get; }
        [Required]
        [Display(Name = "Product Image 2")]
        public IFormFile Photo_2 { set; get; }
        [Required]
        [Display(Name = "Product Image 3")]
        public IFormFile Photo_3 { set; get; }

        [Required]
        [MaxLength(100)]
        [Remote(controller: "Owner" , action: "Same_Title_Post" ,ErrorMessage ="This Title Already Exsist")]
        public String Title { set; get; }

        [Required]
        [MaxLength(1000)]

        public String Description { set; get; }
    }
}
