using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VSMS.Models.ViewModels
{
    public class ProfileViewModels
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        [StringLength(200)]
        public string BirthDay { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(200)]
        public string Phone { get; set; }
    }
}