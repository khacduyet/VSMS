using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models
{
    [Table("Permission_details")]
    public class Permission_details
    {
        [Key, DisplayName("ID"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Detail_Id { get; set; }
        [DisplayName("Hành động")]
        public string Action { get; set; }
        [ForeignKey("Permission")]
        [DisplayName("ID Permission")]
        public int PerId { get; set; }
        public Permission Permission { get; set; }
    }
}