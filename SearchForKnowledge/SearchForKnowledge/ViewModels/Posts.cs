using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using SearchForKnowledge.Models;

namespace SearchForKnowledge.ViewModels
{
    public class PostsIndex
    {
        public List<Post> List { get; set; }
    }

    public class PostsShow
    {
        public Post Post { get; set; }
    } 
    public class PostsShowAll
    {
        public List<Post> Posts { get; set; }
    }
    public class PostsNew
    {
        [Required, MaxLength(128), Display(Name = "Book title")]
        public string BookTitle { get; set; }
        [Required, MaxLength(128)]
        public string Author { get; set; }
        [Required]
        public HttpPostedFileBase ImgFile { get; set; }
        [Required, Display(Name = "User Id")]
        public string UserId { get; set; }
        [Required, Display(Name = "Category of the book")]
        public string CategoryId { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class PostsSearch
    {
        public string ErrorMessage { get; set; }
        public List<Post> Posts { get; set; } 
    }

    public class PostsSelection
    {
        public string ErrorMessage { get; set; }
        public string NameOfCategory { get; set; }
        public List<Post> Posts { get; set; } 
    }
}