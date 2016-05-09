using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace SearchForKnowledge.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string SchoolName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Type { get; set; }

        public void SetPassword(string password)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(password, 13);
        }

        public bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }
    }
}