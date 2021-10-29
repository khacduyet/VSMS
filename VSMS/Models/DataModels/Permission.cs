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
        [Key, DisplayName("ID Permission"), DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PerId { get; set; }
        [DisplayName("Name Permission"), Required(ErrorMessage = "Name Permission not null!")]
        public string PerName { get; set; }
        [DisplayName("Description"), Required(AllowEmptyStrings = true)]
        public string Description { get; set; }
        [DisplayName("Status")]
        public bool Status { get; set; }

        public ICollection<Per_relationship> Per_relationship { get; set; }
    }
}