using System.ComponentModel;

namespace SearchForKnowledge.Models
{
    public class Post
    {
        
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string PicturePath { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }

        public Post(string BookTitle, string Author, string PicturePath, int UserId, int CategoryId, string description) 
        {
            this.BookTitle = BookTitle;
            this.Author = Author;
            this.PicturePath = PicturePath;
            this.UserId = UserId;
            this.CategoryId = CategoryId;
            this.Description = Description;
        }
    }
}