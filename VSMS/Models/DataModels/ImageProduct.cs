using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models.DataModels
{
    [Table("ImageProduct")]
    public class ImageProduct
    {
        [key]
        public int Id { get; set; }

        [DisplayName("Image Name")]
        public string ImageName { get; set; }

        [DisplayName("Status")]
        public byte Status { get; set; }

        public ICollection<ImageProductDetails> ImageProductDetails { get; set; }
        public ImageProduct()
        {
        }

        public ImageProduct(string imageName, byte status)
        {
            ImageName = imageName;
            Status = status;
        }
    }
}