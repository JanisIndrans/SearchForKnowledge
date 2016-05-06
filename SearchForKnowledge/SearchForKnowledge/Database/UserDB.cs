using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using SearchForKnowledge.Models;

namespace SearchForKnowledge.Database
{
    class UserDb
    {
        //public string GetUserByName(string username)
        //{
        //    var mongoClient = new MongoClient("mongodb://localhost");
        //    var database = mongoClient.GetDatabase("SearchForKnowledge");
        //    var coll = database.GetCollection<BsonDocument>("Users");

        //    var filter = Builders<BsonDocument>.Filter.Eq("userName", username);
        //    return filter.ToString();
        //}

        public void UpdateUser(string userName, string password, string schoolName, string country, string city)
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Users");

            var filter = Builders<BsonDocument>.Filter.Eq("userName", userName);
            var update = Builders<BsonDocument>.Update
                //.Set("userName", userName) -----we don't need this as it is not Recomended to change
                .Set("password",password)
                .Set("schoolName", schoolName)
                .Set("country", country)
                .Set("city", city);


            var result = coll.UpdateOne(filter, update);
        }

        public bool AddUser(string userName, string password, string schoolName, string country, string city, string type)
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Users");
            if (!CheckIfUsernameExist(userName))
            {
                var document = new BsonDocument
                {
                    {"userName", userName},
                    {"password", password},
                    {"schoolName", schoolName},
                    {"country", country},
                    {"city", city},
                    {"type", "user"}
                };
                coll.InsertOne(document);
                return true;
            }
            return false;
        }

        public void RemoveUser(string userName)
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Users");

            var filter = Builders<BsonDocument>.Filter.Eq("userName", userName);
            var result = coll.DeleteOne(filter);
        }

        //public string LoginUser(string userName, string pass)
        //{
        //    string result = "";
            
        //    try
        //    {
        //        var mongoClient = new MongoClient("mongodb://localhost");
        //        var database = mongoClient.GetDatabase("SearchForKnowledge");
        //        var coll = database.GetCollection<BsonDocument>("Users");

        //        var filter = Builders<BsonDocument>.Filter.Eq("userName", userName);
        //        var results = coll.Find(filter).ToList().First();
        //        if (BCrypt.Net.BCrypt.Verify(pass, results["password"].ToString()))
        //        {
        //            result = results["userName"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = "";
        //    }
        //    return result;
        //}
        public string GetPassword(string username)
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Users");

            var filter = Builders<BsonDocument>.Filter.Eq("userName", username);
            var results = coll.Find(filter).ToList();
            if (results.Count() != 0)
            {
                var singleResult = results.First();
                return singleResult["password"].ToString();
            }
            return null;
        }
        public string GetType(string username) {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Users");

            var filter = Builders<BsonDocument>.Filter.Eq("userName", username);
            var results = coll.Find(filter).ToList().First();
            return results["type"].ToString();
        }

        public bool CheckIfUsernameExist(string userName)
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");

            var coll = database.GetCollection<BsonDocument>("Users");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Regex("userName", new BsonRegularExpression("/^" + userName + "$/i"));

            if (coll.Find(filter).Count() != 0)
            {
                return true;
            }
            return false;
        }
    }
}
