
using System;
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
        public DateTime CreationDate { get; set; }

        public Post() { 
        }

        public Post(string bookTitle, string author, string picturePath, int userId, CategoryName categoryId, string description) 
        {
            BookTitle = bookTitle;
            Author = author;
            PicturePath = picturePath;
            UserId = userId;
            CategoryId = categoryId;
            Description = description;
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