using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models.DataModels
{
    [Table("Car")]
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Car Name")]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        [StringLength(300)]
        public string CarName { get; set; }

        [ForeignKey("Mode")]
        public int ModeId { get; set; }

        [ForeignKey("Category")]
        public int CatId { get; set; }

        [DisplayName("Engine")]
        [StringLength(200)]
        public string Engine { get; set; } // Động cơ máy ô tô

        [DisplayName("FuelType")]
        [StringLength(200)]
        public string FuelType { get; set; } // Loại nhiên liệu

        [DisplayName("Transmission")]
        [StringLength(200)]
        public string Transmission { get; set; } // Truyền tải

        [DisplayName("Descriptions")]
        public string Descriptions { get; set; }

        [DisplayName("Status")]
        public byte Status { get; set; }

        public Category Category { get; set; }
        public Mode Mode { get; set; }

        public ICollection<CarDetails> CarDetails { get; set; }
        public ICollection<DriveTest> DriveTests { get; set; }
        public ICollection<ImageProductDetails> ImageProductDetails { get; set; }
    }
}