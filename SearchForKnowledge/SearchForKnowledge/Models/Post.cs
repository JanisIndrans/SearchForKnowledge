
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace SearchForKnowledge.Models
{
    public class Post
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string PicturePath { get; set; }
        public int UserId { get; set; }
        public CategoryName CategoryId { get; set; }
        public string Description { get; set; }

        public Post() { 
        }

        public Post(string BookTitle, string Author, string PicturePath, int UserId, CategoryName CategoryId, string Description) 
        {
            this.BookTitle = BookTitle;
            this.Author = Author;
            this.PicturePath = PicturePath;
            this.UserId = UserId;
            this.CategoryId = CategoryId;
            this.Description = Description;
        }

        public enum CategoryName {
            Programming,
            Design,
            Database,
            Security,
            Web,
            SystemAdministration
        }
    }
}