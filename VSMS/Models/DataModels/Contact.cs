using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models.DataModels
{
    [Table("Contact")]
    public class Contact
    {
        [key]
        public int Id { get; set; }
        [DisplayName("Name"), Required(ErrorMessage = "Please enter your name!"), StringLength(150,ErrorMessage = "Too long name!")]
        public string Name { get; set; }
        [DisplayName("Email"), Required(ErrorMessage = "Please enter your email!"), DataType(DataType.EmailAddress,ErrorMessage = "Please enter one email!")]
        public string Email { get; set; }
        [DisplayName("Message"), Required(ErrorMessage = "Please enter your message!")]
        public string Message { get; set; }
        public Nullable<int> CarId { get; set; }
    }
}