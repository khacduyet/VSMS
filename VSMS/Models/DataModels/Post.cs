using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VSMS.Models.DataModels
{
    [Table("Post")]
    public class Post
    {
        [key, DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Title"),Required(ErrorMessage = "This field not null!")]
        public string Title { get; set; }
        [DisplayName("Summary"), Required(ErrorMessage = "This field not null!")]
        public string Summary { get; set; }
        [DisplayName("Content"), Required(ErrorMessage = "This field not null!"),AllowHtml, DataType(DataType.MultilineText), Column(TypeName = "ntext")]
        public string Content { get; set; }
        [DisplayName("Image")]
        public string Image { get; set; }
        [DisplayName("Created At")]
        public DateTime CreatedAt { get; set; }
        [DisplayName("Updated At")]
        public DateTime UpdatedAt { get; set; }
        [DisplayName("Status")]
        public bool Status { get; set; }

        [DisplayName("Author Id")]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Admin Admin { get; set; }
        public ICollection<post_tag> Post_Tags { get; set; }
    }
}