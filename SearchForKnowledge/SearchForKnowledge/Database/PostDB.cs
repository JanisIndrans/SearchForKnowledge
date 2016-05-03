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
    public class PostDb
    {
        public string GetPostByName(string bookTitle)
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Posts");

            var filter = Builders<BsonDocument>.Filter.Eq("BookTitle", bookTitle);
            return filter.ToString();
        }

        public void UpdatePost(string bookTitle, string author, string picturePath, int userId, int categoryId, string description)
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Posts");

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
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Posts");

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
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Posts");

            var filter = Builders<BsonDocument>.Filter.Eq("BookTitle", bookTitle);
            var result = coll.DeleteOne(filter);
        }

        public List<Post> GetAllPosts()
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");

            var coll = new List<Post>(database.GetCollection<Post>("Posts").AsQueryable<Post>());

            return coll;
            /*var coll = database.GetCollection<BsonDocument>("Posts");
            coll.ToJson();

            List<Post> collection = new List<Post>();

            JavaScriptSerializer js = new JavaScriptSerializer();
            collection = (List<Post>)Newtonsoft.Json.JsonConvert.DeserializeObject(coll.ToString());

            return collection;*/

        }
        
    }


}
