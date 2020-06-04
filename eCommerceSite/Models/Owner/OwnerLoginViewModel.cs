using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models.Owner
{
    [NotMapped]
    public class OwnerLoginViewModel
    {

        [Required]
        [MaxLength(50)]
        [RegularExpression("[\\w-]+@([\\w-]+\\.)+[\\w-]+", ErrorMessage = "Email Doesn't Match")]
        public String Email { set; get; }


        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*\\d).{8,50}$", ErrorMessage = "Password Doesn't Match")]
        public String Password { set; get; }


    }
}
