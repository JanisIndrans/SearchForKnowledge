using System.ComponentModel;

namespace SearchForKnowledge.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string PicturePath { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }

        public Post(int id, string bookTitle, string picturePath, int userId, int categoryId, string description)
        {
            Id = id;
            BookTitle = bookTitle;
            PicturePath = picturePath;
            UserId = userId;
            CategoryId = categoryId;
            Description = description;
        }
    }
}