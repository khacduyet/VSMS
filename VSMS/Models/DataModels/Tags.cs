using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models.DataModels
{
    [Table("Tags")]
    public class Tags
    {
        [key]
        public int Id { get; set; }
        [DisplayName("Title"),Required(ErrorMessage = "This field not null!")]
        public string Title { get; set; }
        public string Slug { get; set; }
        [DisplayName("Content")]
        public string Content { get; set; }
    }
}