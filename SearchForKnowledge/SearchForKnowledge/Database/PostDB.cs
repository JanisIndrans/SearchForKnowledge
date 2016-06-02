using System;
using System.Collections.Generic;
using System.EnterpriseServices;
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

        //-----------------------------Start of General methods---------------------------------//

        //Returns Mongo database reference
        public IMongoDatabase GetDatabase()
        {
            var mongoClient = new MongoClient("mongodb://janis:secret@ds017582.mlab.com:17582/searchforknowledge");
            return mongoClient.GetDatabase("searchforknowledge");
        }

        //Returns reference of the Mongo collection
        public IMongoCollection<Post> GetCollectionAsPost()
        {
            return GetDatabase().GetCollection<Post>("Posts");
        }


        //Finds posts for page by filter, page number and posts per page number
        public List<Post> GetPostsForPage(int page, int perPage, FilterDefinition<Post> filter)
        {
            return GetCollectionAsPost()
                .Find(filter)
                .SortByDescending(d => d.CreationDate)
                .Skip((page - 1) * perPage)
                .Limit(perPage)
                .ToList();
        }

        //Counts posts by filter that is passed
        public int CountPosts(FilterDefinition<Post> filter)
        {
            return (int)GetCollectionAsPost()
                .Find(filter)
                .Count();
        }

//-----------------------------------End of general methods-----------------------------------------------------//


//-----------------------------------Start of CRUD methods except the read methods-----------------------------//
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


            var filter = Builders<Post>.Filter.Where(t => t.BookTitle.Equals(post.BookTitle));

            var result = coll.UpdateOne(filter, update);

            return result.IsAcknowledged;
        }

        //Creates one post
        public void CreatePost(Post post)
        {
            var coll = GetDatabase().GetCollection<Post>("Posts");

            coll.InsertOne(post);
            
        }
        //Removes one post from DB
        public void RemovePost(string bookTitle)
        {
            var coll = GetDatabase().GetCollection<Post>("Posts");

            var filter = Builders<Post>.Filter.Eq("BookTitle", bookTitle);
            var result = coll.DeleteOne(filter);
        }


//-----------------------------------End of CRUD methods except the read methods-----------------------------//

//-----------------------------------Start of Read methods---------------------------------------------------//

        //------------------------Methods for all posts------------------------//

        //Returns number of all posts in the collection
        public int NumberOfAllPosts()
        {
            return CountPosts(FilterDefinition<Post>.Empty);
        }
        //Returns the list of posts for specific page
        public List<Post> GetAllPostsForPage(int page, int perPage)
        {
            return GetPostsForPage(page, perPage, FilterDefinition<Post>.Empty);
        }
        //-----------------------Methods for Category posts---------------------//

        //Returns number of all category posts
        public int NumberOfCategoryPosts(Post.CategoryName categoryName)
        {
            var filter = Builders<Post>.Filter.Where(c => c.CategoryId == categoryName);
            return CountPosts(filter);
        }
        //Returns the list of posts from the specified category for specific page 
        public List<Post> GetPostsForCategoryPage(Post.CategoryName categoryName, int page, int perPage)
        {
            var filter = Builders<Post>.Filter.Where(c => c.CategoryId == categoryName);
            return GetPostsForPage(page, perPage, filter);
        }

        //-----------------------Methods for search posts--------------------------//

        //Returns number of all posts that contains searched string in title
        public int NumberOfSearchedPosts(string searchString)
        {
            var filter = Builders<Post>.Filter.Where(t => t.BookTitle.ToLower().Contains(searchString.ToLower()));
            return CountPosts(filter);
        }
        //Returns the list of posts for the searched title for specific page 
        public List<Post> GetPostsForSearchPage(string searchString, int page, int perPage)
        {
            var filter = Builders<Post>.Filter.Where(t => t.BookTitle.ToLower().Contains(searchString.ToLower()));
            return GetPostsForPage(page,perPage,filter);
        }
        //----------------------Methods for User's posts-----------------------------//

        //Returns number of all posts for specific user
        public int NumberOfUsersPosts(string name)
        {
            var filter = Builders<Post>.Filter.Where(n => n.Username.Equals(name));
            return CountPosts(filter);
        }
        //Returns the list of posts of specific user for specific page 
        public List<Post> GetPostsForUsersPage(string name, int page, int perPage)
        {
            var filter = Builders<Post>.Filter.Where(n => n.Username.Equals(name));
            return GetPostsForPage(page,perPage,filter);
        }
//-----------------------------------------End of read methods---------------------------------------------------------//


        //Method for Populating database
        //public void Seed()
        //{
        //    List<Post> list = new List<Post>();
        //    var coll = GetDatabase().GetCollection<Post>("Posts");
        //    for (int x = 0; x < 30; x++)
        //    {
        //        Post post = new Post
        //        {
        //            BookTitle = "Seedy",
        //            CreationDate = DateTime.Now,
        //            CategoryId = Post.CategoryName.Web,
        //            Username = "janix",
        //            Author = "Seedy",
        //            Description = "Just testing and seeding the db",
        //        };
        //        list.Add(post);
        //    }
        //    coll.InsertMany(list);
        //}

    }

}
