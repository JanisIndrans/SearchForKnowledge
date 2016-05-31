using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using SearchForKnowledge.Infrastructure;
using SearchForKnowledge.Models;

namespace SearchForKnowledge.ViewModels
{

    public class PostsDisplay
    {
        public PagedData<Post> Posts { get; set; }
        public List<Post> PostsToDisplay { get; set; }
        public string ErrorMessage { get; set; }
        public string SearchString { get; set; }
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
        public int UserId { get; set; }
        [Required, Display(Name = "Category of the book")]
        public Post.CategoryName CategoryId { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class PostsCategory
    {
        public string ErrorMessage { get; set; }
        public Post.CategoryName NameOfCategory { get; set; }
        public PagedData<Post> Posts { get; set; }
        public List<Post> PostsToDisplay { get; set; }
    }
}