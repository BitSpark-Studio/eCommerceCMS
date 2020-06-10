using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models.Admin
{
    [NotMapped]
    public class Owner_ViewList_Model
    {
        public int OID { set; get; }
        [Display(Name="Name")]
        public String First_Name { set; get; }

        public String Email { set; get; }

        public String Phone { set; get; }

        public String Photo { set; get; }
        
    }
}

