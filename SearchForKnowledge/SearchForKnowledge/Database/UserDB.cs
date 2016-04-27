﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using SearchForKnowledge.Models;

namespace SearchForKnowledge
{
    class UserDB
    {
        public string getUserByName(string username)
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Users");

            var filter = Builders<BsonDocument>.Filter.Eq("userName", username);
            return filter.ToString();
        }

        public void updateUser(string userName, string schoolName, string country, string city)
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Users");

            var filter = Builders<BsonDocument>.Filter.Eq("userName", userName);
            var update = Builders<BsonDocument>.Update
                .Set("userName", userName)
                .Set("schoolName", schoolName)
                .Set("country", country)
                .Set("city", city);


            var result = coll.UpdateOne(filter, update);
        }

        public void addUser(string userName, string password, string schoolName, string country, string city)
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Users");

            var document = new BsonDocument
                {
                    {"userName",userName},
                    {"password",password},
                    {"schoolName",schoolName},
                    {"country",country},
                    {"city",city}
                };
            coll.InsertOne(document);
        }

        public void removeUser(string userName)
        {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Users");

            var filter = Builders<BsonDocument>.Filter.Eq("userName", userName);
            var result = coll.DeleteOne(filter);
        }

        public string loginUser(string userName, string pass)
        {
            string result = "";
            
            try
            {
                var mongoClient = new MongoClient("mongodb://localhost");
                var database = mongoClient.GetDatabase("SearchForKnowledge");
                var coll = database.GetCollection<BsonDocument>("Users");

                var filter = Builders<BsonDocument>.Filter.Eq("userName", userName);
                var results = coll.Find(filter).ToList().First();
                if (BCrypt.Net.BCrypt.Verify(pass, results["password"].ToString()))
                {
                    result = results["userName"].ToString();
                }
            }
            catch (Exception ex)
            {
                result = "";
            }
            return result;
        }
        public string getPassword(string username) {
            var mongoClient = new MongoClient("mongodb://localhost");
            var database = mongoClient.GetDatabase("SearchForKnowledge");
            var coll = database.GetCollection<BsonDocument>("Users");

            var filter = Builders<BsonDocument>.Filter.Eq("userName", username);
            var results = coll.Find(filter).ToList().First();
            return results["password"].ToString();
        }
    }
}
