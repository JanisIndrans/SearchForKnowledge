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
    public class PostDB
    {
        public string getPostByName(string BookTitle)
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Posts");

            var filter = Builders<BsonDocument>.Filter.Eq("BookTitle", BookTitle);
            return filter.ToString();
        }

        public void updatePost(string BookTitle, string Author, string PicturePath, int UserId, int CategoryId, string description)
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Posts");

            var filter = Builders<BsonDocument>.Filter.Eq("BookTitle", BookTitle);
            var update = Builders<BsonDocument>.Update
                .Set("BookTitle", BookTitle)
                .Set("Author", Author)
                .Set("PicturePath", PicturePath)
                .Set("UserId", UserId)
                .Set("CategoryId", CategoryId)
                .Set("Description", description);
            var result = coll.UpdateOne(filter, update);
        }

        public void createPost(string BookTitle, string Author, string PicturePath, int UserId, int CategoryId, string description)
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Posts");

            var document = new BsonDocument
                {
                    {"BookTitle",BookTitle},
                    {"Author",Author},
                    {"PicturePath",PicturePath},
                    {"UserId",UserId},
                    {"CategoryId", CategoryId},
                    {"Description", description}
                };
            coll.InsertOne(document);
        }

        public void removePost(string BookTitle)
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Posts");

            var filter = Builders<BsonDocument>.Filter.Eq("BookTitle", BookTitle);
            var result = coll.DeleteOne(filter);
        }

        public List<Post> getAllPosts()
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");

            var coll = new List<Post>(database.GetCollection<Post>("Post").AsQueryable<Post>());

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
