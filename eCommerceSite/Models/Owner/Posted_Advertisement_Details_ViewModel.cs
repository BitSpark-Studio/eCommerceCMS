using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models.Owner
{
    [NotMapped]
    public class Posted_Advertisement_Details_ViewModel
    {

        [Display(Name="Product Title")]
        public String Product_Title { set; get; }
        [Display(Name = "Product Description")]
        public String Product_Description { set; get; }
        [Display(Name ="Cetagory")]
        public String Cetagory_Name { set; get; }

        public String Cetagory_Image { set; get; }

        public int CID { set; get; }

        public int PID { set; get; }
    }
}
