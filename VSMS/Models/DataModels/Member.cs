using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models.DataModels
{
    [Table("Member")]
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("FullName")]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        [StringLength(200)]
        public string FullName{ get; set; }

        [DisplayName("UserName")]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        [StringLength(200)]
        public string UserName{ get; set; }

        [DisplayName("PassWord")]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        [StringLength(200)]
        public string PassWord{ get; set; }

        [DisplayName("BirthDay")]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        [StringLength(200)]
        public string BirthDay{ get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        [StringLength(200)]
        public string Email{ get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        [StringLength(200)]
        public string Address{ get; set; }

        [DisplayName("Phone")]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        [StringLength(200)]
        public string Phone{ get; set; }
        public DateTime CreatedAt{ get; set; }
        public byte Status{ get; set; }
        public ICollection<DriveTest> DriveTests{ get; set; }
        public ICollection<Order> Orders{ get; set; }
    }
}