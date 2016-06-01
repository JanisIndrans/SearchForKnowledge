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

        //Returns Mongo database
        public IMongoDatabase GetDatabase()
        {
            var mongoClient = new MongoClient("mongodb://janis:secret@ds017582.mlab.com:17582/searchforknowledge");
            return mongoClient.GetDatabase("searchforknowledge");
        }

        //Returns the Mongo collection creating Post objects
        public IMongoCollection<Post> GetCollectionAsPost()
        {
            return GetDatabase().GetCollection<Post>("Posts");
        }


        //Returns the filter by book title
        public FilterDefinition<Post> GetFilterByBookTitle(string bookTitle)
        {
            var filter = Builders<Post>.Filter.Eq("BookTitle", bookTitle);
            return filter;
        }


        //Updates one post
        public bool UpdatePost(Post post)
        {
            var coll = GetDatabase().GetCollection<Post>("Posts");

            var update = Builders<Post>.Update
                .Set("BookTitle", post.BookTitle)
                .Set("Author", post.Author)
                .Set("PicturePath", post.PicturePath)
                .Set("UserId", post.Username)
                .Set("CategoryId", post.CategoryId)
                .Set("Description", post.Description)
                .Set("CreationDate", post.CreationDate);

            var result = coll.UpdateOne(GetFilterByBookTitle(post.BookTitle), update);

            return result.IsAcknowledged;
        }
        //Creates post
        public void CreatePost(Post post)
        {
            var coll = GetDatabase().GetCollection<Post>("Posts");

            coll.InsertOne(post);
            
        }
        //Removes post from DB
        public void RemovePost(string bookTitle)
        {
            var coll = GetDatabase().GetCollection<Post>("Posts");

            var filter = Builders<Post>.Filter.Eq("BookTitle", bookTitle);
            var result = coll.DeleteOne(filter);
        }

        //Finds all posts by Book title in matter that it checks if any book title contains the given string ignorig case sensetivity
        public List<Post> GetSearchResult(string bookTitle)
        {
            var coll = GetCollectionAsPost()
                .Find(x=>x.BookTitle.ToLower()
                    .Contains(bookTitle.ToLower()))
                    .ToList();

            if (coll.Count() != 0)
            {
                return coll;
            }
            return null;
        }
        //Returns all posts for the category
        public List<Post> GetPostsByCategory(Post.CategoryName categoryName)
        {
            var coll = GetCollectionAsPost()
                 .Find(x => x.CategoryId == categoryName)
                     .ToList();

            if (coll.Count() != 0)
            {
                return coll;
            }
            return null;
        }
        //Returns all posts from db
        public List<Post> GetAllPosts()
        {
          return new List<Post>(GetDatabase().GetCollection<Post>("Posts").AsQueryable());
        }


        public List<Post> GetAllUsersPosts(string name)
        {
            var coll = GetCollectionAsPost()
                 .Find(x => x.Username == name)
                     .ToList();

            if (coll.Count() != 0)
            {
                return coll;
            }
            return null;
        }
    
    }

}
