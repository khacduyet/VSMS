using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models.DataModels
{
    [Table("ImageProductDetails")]
    public class ImageProductDetails
    {
        [key]
        public int Id { get; set; }

        [ForeignKey("ImageProduct")]
        public int IdImageProduct { get; set; }

        [ForeignKey("Car")]
        public int IdProduct { get; set; }

        [DisplayName("Status")]
        public byte Status { get; set; }

        public ImageProduct ImageProduct { get; set; }
        public Car Car { get; set; }

        public ImageProductDetails()
        {
        }

        public ImageProductDetails(int idImageProduct, int idProduct, byte status)
        {
            IdImageProduct = idImageProduct;
            IdProduct = idProduct;
            Status = status;
        }

    }
}