using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required, MaxLength(128)]
        public string BookTitle { get; set; }
        [Required, MaxLength(128)]
        public string Author { get; set; }
        [Required]
        public string PicturePath { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}