using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models.Owner
{
    [NotMapped]
    public class OwnerRegistrationViewModel
    {
     

        [Required]
        [MinLength(3, ErrorMessage = "First Name Must be in between 3-20 character")]
        [MaxLength(20, ErrorMessage = "First Name Must be in between 3-20 character")]
        [Display(Name = "First Name")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "The First Name Only Allows Alphabet and No Space")]
        [Column(TypeName = "varchar(20)")]
        public String First_Name { set; get; }
        [Required]
        [MinLength(3, ErrorMessage = "Last Name Must be in between 3-20 character")]
        [MaxLength(20, ErrorMessage = "Last Name Must be in between 3-20 character")]
        [Display(Name = "Last Name")]
        [RegularExpression("^[a-zA-Z]+(([\'\\,\\.\\- ][a-zA-Z ])?[a-zA-Z]*)*$")]
        [Column(TypeName = "varchar(20)")]
        public String Last_Name { set; get; }

        [Required]
        [MaxLength(50)]
        [RegularExpression("[\\w-]+@([\\w-]+\\.)+[\\w-]+", ErrorMessage = "Invalid Email")]
        [Remote(action:"SameEmail",controller:"Owner" , ErrorMessage ="This Email is Already Has An Identity")]
        public String Email { set; get; }

        [Required]
        [MaxLength(20, ErrorMessage = "Phone Number Can not be this long")]
        [RegularExpression("([+]?\\d[ ]?[(]?\\d{3}[)]?[ ]?\\d{2,3}[- ]?\\d{2}[- ]?\\d{2})", ErrorMessage = "Invalid Phone Number")]
        [Display(Name = "Phone Number")]
        public String Phone { set; get; }

        [Required]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public String Address { set; get; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*\\d).{8,50}$", ErrorMessage = "Password must be between 8 and 50 digits long and include at least one numeric digit.")]
        public String Password { set; get; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confrim Passowrd")]
        public String Confrim_Password { set; get; }

        [NotMapped]
        [Required]
        [Display(Name="Upload Profile Picture")]
        public IFormFile Photo { set; get; }

        public bool Policy { set; get; }

       
    }
}
