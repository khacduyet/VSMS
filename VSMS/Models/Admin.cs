using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models
{
    [Table("Admin")]
    public class Admin
    {
        [Key, DisplayName("ID"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Tên tài khoản"), Required(ErrorMessage = "Tên tài khoản không được để trống!"), StringLength(20, ErrorMessage = "Tên tài khoản bao gồm từ 3-20 kí tự!",MinimumLength = 3)]
        public string Username { get; set; }
        [DisplayName("Mật khẩu"), Required(ErrorMessage = "Mật khẩu không được để trống!")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Mật khẩu tối thiểu tám ký tự, ít nhất một chữ cái và một số!")]
        public string Password { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Chưa xác nhận mật khẩu!")]
        [DisplayName("Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không trùng khớp!")]
        public string ConfirmPassword { get; set; }
        [DisplayName("Họ tên"), Required(ErrorMessage = "Họ tên không được để trống!")]
        public string Name { get; set; }
        [DisplayName("Email"), Required(ErrorMessage = "Email không được để trống!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage = "Email không hợp lệ!")]
        public string Email { get; set; }
        [DisplayName("Ảnh đại diện")]
        public string Avatar { get; set; }
        [DisplayName("Trạng thái")]
        public bool Status { get; set; }

        public ICollection<Per_relationship> Per_relationship { get; set; }
    }
}