﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models.Cetagory
{
    [NotMapped]
    public class HomeCategoryViewModel
    {
        [Key]
        public int CID { set; get; }
        public string Category_Title { get; set; }
        public string Category_Image { get; set; }

    }
}
