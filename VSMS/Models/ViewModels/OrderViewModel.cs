using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VSMS.Models.ViewModels
{
    public class OrderViewModel
    {
        public int MemberId { get; set; }
        public int CarId { get; set; }
        public int Quantity { get; set; }
        public float Total { get; set; }

    }
}