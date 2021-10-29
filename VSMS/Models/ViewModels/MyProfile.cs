using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VSMS.Models.ViewModels
{
    public class MyProfile
    {
        public int Id { get; set; }
        [DisplayName("UserName"), Required(ErrorMessage = "User name not null!"), StringLength(20, ErrorMessage = "User name included from 3-20 characters!", MinimumLength = 3)]
        public string Username { get; set; }
        [DisplayName("Full Name"), Required(ErrorMessage = "Full name is not left blank!")]
        public string Name { get; set; }
        [DisplayName("Email"), Required(ErrorMessage = "Email not null!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email!")]
        public string Email { get; set; }
        [DisplayName("Avatar")]
        public string Avatar { get; set; }
    }
}