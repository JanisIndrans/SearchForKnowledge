using System.Collections.Generic;
using System.Web.Mvc;
using SearchForKnowledge.Models;
using SearchForKnowledge.ViewModels;
using SearchForKnowledge.Database;
using System;
using System.IO;
using System.Linq;
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
            List<Post> posts = db.GetAllPosts();
            var totalPostCount = posts.Count;
            var currentPostPage = GetPostsForPage(posts, page);


            return View(new PostsDisplay
            {
                Posts = new PagedData<Post>(currentPostPage, totalPostCount, page, PostsPerPage),
                PostsToDisplay = currentPostPage

            });
        }

        public List<Post> GetPostsForPage(List<Post> posts, int page)
        {
            return posts.OrderByDescending(d => d.CreationDate)
               .Skip((page - 1) * PostsPerPage)
               .Take(PostsPerPage)
               .ToList();
        }


        public ActionResult SearchPosts(string searchString = "", int page = 1)
        {
            PostDb db = new PostDb();

            int totalPostCount = 0;
            var currentPostPage = new List<Post>();
            var searchList = db.GetSearchResult(searchString);

            if (searchList != null)
            {
                totalPostCount = searchList.Count;
                currentPostPage = GetPostsForPage(searchList, page);
            }

            if (totalPostCount != 0)
            {
                return View(new PostsDisplay
                {

                    Posts = new PagedData<Post>(currentPostPage, totalPostCount, page, PostsPerPage),
                    PostsToDisplay = currentPostPage,
                    SearchString = searchString

                });
            }

            return View(new PostsDisplay
            {
                ErrorMessage = "Sorry nothing was found with this title",
                Posts = new PagedData<Post>(currentPostPage, totalPostCount, page, PostsPerPage),
                PostsToDisplay = currentPostPage
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


            PostDb db = new PostDb();

            List<Post> list = new List<Post>();
            string path = "";
            if (form.ImgFile != null)
            {

                path = AddImage(form.ImgFile);
            }

            Post post = new Post
            {
                BookTitle = form.BookTitle,
                Author = form.Author,
                PicturePath = path,
                UserId = form.UserId,
                CategoryId = form.CategoryId,
                Description = form.Description,
                CreationDate = DateTime.Now                
            };
               
            db.CreatePost(post);
            
            return RedirectToRoute("Home");
        }


        public ActionResult Category(Post.CategoryName category, int page = 1)
        {
            PostDb db = new PostDb();

            int totalPostCount = 0;
            var currentPostPage = new List<Post>();
            var list = db.GetPostsByCategory(category);

            if (list != null)
            {
                totalPostCount = list.Count;
                currentPostPage = GetPostsForPage(list, page);
            }

            if (totalPostCount != 0)
            {
                return View(new PostsCategory
                {

                    Posts = new PagedData<Post>(currentPostPage, totalPostCount, page, PostsPerPage),
                    PostsToDisplay = currentPostPage,
                    NameOfCategory = category

                });
            }

            return View(new PostsCategory
            {
                ErrorMessage = "Sorry nothing was found in this Category",
                Posts = new PagedData<Post>(currentPostPage, totalPostCount, page, PostsPerPage),
                PostsToDisplay = currentPostPage,
                NameOfCategory = category
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
