using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using VSMS.Models.DataModels;

namespace VSMS.Models
{
    [Table("Admin")]
    public class Admin
    {
        [Key, DisplayName("ID"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("UserName"), Required(ErrorMessage = "User name not null!"), StringLength(20, ErrorMessage = "User name included from 3-20 characters!", MinimumLength = 3)]
        public string Username { get; set; }
        [DisplayName("Password"), Required(ErrorMessage = "Password not null!")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password Minimum eight characters, at least one letter and some!")]
        public string Password { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Not confirmed password!")]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Password does not match!")]
        public string ConfirmPassword { get; set; }
        [DisplayName("Full Name"), Required(ErrorMessage = "Full name is not left blank!")]
        public string Name { get; set; }
        [DisplayName("Email"), Required(ErrorMessage = "Email not null!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage = "Invalid email!")]
        public string Email { get; set; }
        [DisplayName("Avatar")]
        public string Avatar { get; set; }
        [DisplayName("Status")]
        public bool Status { get; set; }

        public ICollection<Per_relationship> Per_relationship { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}