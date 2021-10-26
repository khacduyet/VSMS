using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models.DataModels
{
    [Table("Mode")]
    public class Mode
    {
        [Key]
        public int Id{ get; set; }

        [DisplayName("ModeName")]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        [StringLength(300)]
        public string ModeName{ get; set; }

        [DisplayName("Year")]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        public int Year{ get; set; }

        [DisplayName("Note")]
        [StringLength(500)]
        public string Note{ get; set; }

        [ForeignKey("Manuafature")]
        [DisplayName("Manuafature Name")]
        public int ManafatureId { get; set; }

        [DisplayName("Status")]
        public byte Status{ get; set; }

        public Manuafature Manuafature{ get; set; }
        public ICollection<Car> Cars{ get; set; }

    }
}