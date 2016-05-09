using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using SearchForKnowledge.Models;

namespace SearchForKnowledge.Database
{
    class UserDb
    {

        public IMongoDatabase GetDatabase()
        {
            var mongoClient = new MongoClient("mongodb://janis:secret@ds017582.mlab.com:17582/searchforknowledge");
            return mongoClient.GetDatabase("searchforknowledge");
        }

        public void UpdateUser(string username, string password, string schoolName, string country, string city)
        {
            var coll = GetDatabase().GetCollection<User>("Users");

            var filter = Builders<User>.Filter.Eq("Username", username);
            var update = Builders<User>.Update
                .Set("Password",password)
                .Set("SchoolName", schoolName)
                .Set("Country", country)
                .Set("City", city);


            var result = coll.UpdateOne(filter, update);
        }

        public bool AddUser(User user)
        {
            var coll = GetDatabase().GetCollection<User>("Users");
            if (GetUserByUsername(user.Username) == null)
            {
                coll.InsertOne(user);
                return true;
            }
            return false;
        }

        public bool RemoveUser(string username)
        {
            var coll = GetDatabase().GetCollection<User>("Users");

            var filter = Builders<User>.Filter.Eq("Username", username);

            return coll.DeleteOne(filter).IsAcknowledged;

        }

        //public string GetPassword(string username)
        //{
        //    var coll = GetDatabase().GetCollection<User>("Users");

        //    var filter = Builders<User>.Filter.Eq("Username", username);
        //    var result = coll.Find(filter).FirstOrDefault();
        //    if (result != null)
        //    {
        //        return result.Password;
        //    }
        //    return null;
        //}

        public User GetCurrentUser()
        {
            var coll = GetDatabase().GetCollection<User>("Users");

            var filter = Builders<User>.Filter.Eq("Username", HttpContext.Current.User.Identity.Name);
            var result = coll.Find(filter).FirstOrDefault();

            return result;
        }

        public User GetUserByUsername(string username)
        {
            var coll = GetDatabase().GetCollection<User>("Users");
            var filter = Builders<User>.Filter.Eq("Username", new BsonRegularExpression("/^" + username + "$/i"));

            return coll.Find(filter).FirstOrDefault();
        }
    }
}
