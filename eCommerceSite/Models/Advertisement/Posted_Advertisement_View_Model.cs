using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models.Advertisement
{
    [NotMapped]
    public class Posted_Advertisement_View_Model
    {
        public String Product_Image_1 { set; get; }

        public String Product_Image_2 { set; get; }

        public String Product_Image_3 { set; get; }

        public String Cetagory_Image { set; get; }

        public String Cetagory_Title { set; get; }

        public String Cetagory_Description { set; get; }

        public String Product_Title { set; get; }

        public String Product_Description { set; get; }
    }
}
