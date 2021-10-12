using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models
{
    [Table("Permission")]
    public class Permission
    {
        [Key, DisplayName("Mã quyền"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PerId { get; set; }
        [DisplayName("Tên quyền"), Required(ErrorMessage = "Tên quyền không được để trống!")]
        public string PerName { get; set; }
        [DisplayName("Mô tả"), Required(AllowEmptyStrings = true)]
        public string Description { get; set; }
        [DisplayName("Trạng thái")]
        public bool Status { get; set; }

        public ICollection<Permission_details> Permission_details { get; set; }
        public ICollection<Per_relationship> Per_relationship { get; set; }
    }
}