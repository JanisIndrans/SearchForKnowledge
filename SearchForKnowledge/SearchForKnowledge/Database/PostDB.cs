using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using SearchForKnowledge.Models;
using System.Web.Script.Serialization;

using Newtonsoft.Json;

namespace SearchForKnowledge.Database
{
    public class PostDb {


        public IMongoDatabase GetDatabase()
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            return mongoClient.GetDatabase("SearchForKnowledge");
        }


        public string GetPostByName(string bookTitle)
        {
            var coll = GetDatabase().GetCollection<BsonDocument>("Posts");

            var filter = Builders<BsonDocument>.Filter.Eq("BookTitle", bookTitle);
            return filter.ToString();
        }

        public void UpdatePost(string bookTitle, string author, string picturePath, int userId, int categoryId, string description)
        {
            var coll = GetDatabase().GetCollection<BsonDocument>("Posts");

            var filter = Builders<BsonDocument>.Filter.Eq("BookTitle", bookTitle);
            var update = Builders<BsonDocument>.Update
                .Set("BookTitle", bookTitle)
                .Set("Author", author)
                .Set("PicturePath", picturePath)
                .Set("UserId", userId)
                .Set("CategoryId", categoryId)
                .Set("Description", description);
            var result = coll.UpdateOne(filter, update);
        }

        public void CreatePost(string bookTitle, string author, string picturePath, int userId, int categoryId, string description)
        {
            var coll = GetDatabase().GetCollection<BsonDocument>("Posts");

            var document = new BsonDocument
                {
                    {"BookTitle",bookTitle},
                    {"Author",author},
                    {"PicturePath",picturePath},
                    {"UserId",userId},
                    {"CategoryId", categoryId},
                    {"Description", description}
                };
            coll.InsertOne(document);
        }

        public void RemovePost(string bookTitle)
        {
            var coll = GetDatabase().GetCollection<BsonDocument>("Posts");

            var filter = Builders<BsonDocument>.Filter.Eq("BookTitle", bookTitle);
            var result = coll.DeleteOne(filter);
        }

        public List<Post> GetAllPosts()
        {
            var coll = new List<Post>(GetDatabase().GetCollection<Post>("Posts").AsQueryable<Post>());

            return coll;

        }

        public List<Post> GetSearchResults(string bookTitle)
        {
            var coll = new List<Post>(GetDatabase().GetCollection<Post>("Posts").AsQueryable<Post>());
            List<Post> posts = new List<Post>();
            if (coll.Count() != 0) { 
                foreach (Post post in coll)
                {
                    if (post.BookTitle.ToLower().Contains(bookTitle.ToLower()))
                    {
                        posts.Add(post);
                    }
                }
                if (posts.Count() != 0)
                {
                    return posts;  
                }
            }
            return null;
        }

        //public List<Post> getAllProgramming()
        //{
        //    var result = new List<Post>();

        //    var mongoClient = new MongoClient("mongodb://localhost");
        //    var database = mongoClient.GetDatabase("SearchForKnowledge");
        //    var coll = database.GetCollection<Post>("Posts");

        //    var filter = Builders<Post>.Filter.Eq(p => p.CategoryId, 1);
        //    return result = coll.Find(filter).ToList();
        //}

        public List<Post> GetPostsByCategory(string categoryName)
        {
            var result = new List<Post>();


            var coll = GetDatabase().GetCollection<Post>("Posts");

            var filter = Builders<Post>.Filter.Eq(p => p.CategoryId, 1);
            return result = coll.Find(filter).ToList();
        }

        public int CountAllPosts()
        {
            var coll = new List<Post>(GetDatabase().GetCollection<Post>("Posts").AsQueryable());
            return coll.Count();
        }

        public List<Post> GetCurrentPagePosts(int page, int postsPerPage)
        {
            var result = new List<Post>();

            var coll = GetDatabase().GetCollection<Post>("Posts");

            return result = coll.Find(FilterDefinition<Post>.Empty)
                .Skip((page - 1)*postsPerPage)
                .Limit(postsPerPage)
                .ToList();
        } 

        
    }


}
