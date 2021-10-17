using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VSMS.Models.ViewModels
{
    public class AccountViewModels
    {
        
    }

    public class ChangePassword
    {
        public int Id { get; set; }
        [DisplayName("UserName"), Required(ErrorMessage = "User name not null!"), StringLength(20, ErrorMessage = "User name included from 3-20 characters!", MinimumLength = 3)]
        public string Username { get; set; }
        [DisplayName("Password"), Required(ErrorMessage = "Password not null!")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password Minimum eight characters, at least one letter and some!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Not confirmed password!")]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Password does not match!")]
        public string ConfirmPassword { get; set; }
    }

}