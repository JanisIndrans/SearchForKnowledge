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
}