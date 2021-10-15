using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models.DataModels
{
    [Table("Category")]
    public class Category
    {
        [key]
        public int Id{ get; set; }

        [DisplayName("Category Name")]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        [StringLength(300)]
        public string CateName{ get; set; }

        [DisplayName("Note")]
        [StringLength(500)]
        public string Note{ get; set; }

        [DisplayName("Status")]
        public byte Status{ get; set; }

        public ICollection<Car> Cars{ get; set; }
    }
}