using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models.Customer
{
    [NotMapped]
    public class ProductCetagoryOwner
    {
        public String Product_Title { set; get; }

        public String Product_Description { set; get; }

        public String Cetagory_Title { set; get; }

        public String Cetagory_Details { set; get; }

        public String Cetagory_Photo { set; get; }

        public int PID { set; get; }

        public int OID { set; get; }

        
    }
}
