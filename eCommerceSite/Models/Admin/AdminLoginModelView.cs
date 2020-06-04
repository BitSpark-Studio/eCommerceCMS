using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models.Admin
{
    [NotMapped]
    public class AdminLoginModelView
    {
        
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("[\\w-]+@([\\w-]+\\.)+[\\w-]+", ErrorMessage = "Invalid Email")]
        public String Email { set; get; }

        [MaxLength(50)]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*\\d).{8,50}$", ErrorMessage = "Password must be between 8 and 50 digits long and include at least one numeric digit.")]
        public String Password { set; get; }
    }
}
