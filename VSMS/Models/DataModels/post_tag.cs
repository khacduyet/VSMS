using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VSMS.Models.DataModels
{
    [Table("Post_Tags")]
    public class post_tag
    {
        [key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public string TagId { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }

        [NotMapped]
        public string[] selectedIdArray { get; set; }
    }
}