using System.Collections.Generic;
using System.Web.Mvc;
using SearchForKnowledge.Models;
using SearchForKnowledge.ViewModels;
using SearchForKnowledge.Database;
using System;
using System.IO;
using System.Web;
using Microsoft.Ajax.Utilities;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SearchForKnowledge.Infrastructure;

namespace SearchForKnowledge.Controllers
{
    public class PostsController : Controller
    {

        private const int  PostsPerPage = 14;

        public ActionResult Index(int page = 1)
        {
            PostDb db = new PostDb();
            //List<Post> posts = db.GetAllPosts();
            var totalPostCount = db.CountAllPosts();
            var currentPostPage = db.GetCurrentPagePosts(page, PostsPerPage);
            

            return View(new PostsShowAll
            {
                Posts = new PagedData<Post>(currentPostPage, currentPostPage, totalPostCount, page, PostsPerPage)

            });
        }

        public ActionResult SearchPosts()
        {

            return View(new PostsSearch
            {

            });
        }
        [HttpPost]
        public ActionResult SearchPosts(string searchString)
        {
            PostDb db = new PostDb();
            List<Post> posts = db.GetSearchResults(searchString);
            if (posts != null)
            {
                return View(new PostsSearch
                {
                    
                    Posts = posts

                });
            }
            return View(new PostsSearch
            {
                ErrorMessage = "Sorry nothing was found with this title",
                Posts = new List<Post>()
            });
        }

        public ActionResult CreatePost()
        {

            return View(new PostsNew
            {

            });
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreatePost(PostsNew form)
        {

            //test
            //test


            PostDb db = new PostDb();
            //string imgPath = "";
            if (form.ImgFile != null)
            {
                string path = AddImage(form.ImgFile);
               
                Post post = new Post();
                post.BookTitle = form.BookTitle;
                post.Author = form.Author;
                post.PicturePath = path;
                post.UserId = form.UserId;
                post.CategoryId = (SearchForKnowledge.Models.Post.CategoryName)form.CategoryId;
                post.Description = form.Description;

                db.CreatePost(post);
            }
            return RedirectToRoute("Home");
        }


        public ActionResult Category()
        {

            return View(new PostsSelection()
            {

            });
        }
        [HttpPost]
        public ActionResult Category(SearchForKnowledge.Models.Post.CategoryName categoryName, int page = 1)
        {
            PostDb db = new PostDb();
            
            var postCount = db.CountCategoryPosts(categoryName);
            //var currentPostPage = db.GetCurrentPagePosts(page, PostsPerPage);
            List<Post> currentPostPage = db.GetPostsByCategory(categoryName, page, PostsPerPage);
            if (currentPostPage != null)
            {
                return View(new PostsSelection()
                {

                    Posts = new PagedData<Post>(currentPostPage, currentPostPage, postCount, page, PostsPerPage),
                    NameOfCategory = categoryName
                   

                });
            }
            return View(new PostsSelection()
            {
                ErrorMessage = "Sorry nothing was found with this selection",
                Posts = new PagedData<Post>()
            });
        }

        public string AddImage(HttpPostedFileBase image)
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("searchforknowledge");

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            container.SetPermissions(
             new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            // Retrieve reference to a blob named "myblob".
            string newFileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(image.FileName);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(newFileName);
            blockBlob.Properties.ContentType = image.ContentType;
            blockBlob.UploadFromStream(image.InputStream);

            var uriBuilder = new UriBuilder(blockBlob.Uri);
            uriBuilder.Scheme = "https";
            return uriBuilder.ToString();
        }

        }

    }
