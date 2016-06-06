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

        private const int PostsPerPage = 14;


//-----------------------Start of Controllers for displaying posts in various search ways and by categories----------------------------//
        public ActionResult Index(int page = 1)
        {

            PostDb db = new PostDb();
            
            List<Post> currentPostPage = db.GetAllPostsForPage(page, PostsPerPage);
            int numberOfPosts = db.NumberOfAllPosts();
            if (numberOfPosts != 0)
            {
                
                return View(new PostsDisplay
                {
                    Posts = new PagedData<Post>(currentPostPage, numberOfPosts, page, PostsPerPage),
                    PostsToDisplay = currentPostPage

                });
            }

            else
            {
                return View(new PostsDisplay
                {
                    Posts = new PagedData<Post>(currentPostPage, numberOfPosts, page, PostsPerPage),
                    PostsToDisplay = currentPostPage,
                    ErrorMessage = "Sorry, there are no posts at the moment!"

                });
            }
        }

        public ActionResult SearchPosts(string searchString = "", int page = 1)
        {
            PostDb db = new PostDb();

            List<Post> currentPostPage = db.GetPostsForSearchPage(searchString, page, PostsPerPage);
            int numberOfPosts = db.NumberOfSearchedPosts(searchString);

            if (numberOfPosts != 0)
            {
                
                return View(new PostsDisplay
                {

                    Posts = new PagedData<Post>(currentPostPage, numberOfPosts, page, PostsPerPage),
                    PostsToDisplay = currentPostPage,
                    SearchString = searchString

                });
            }
            else
            {
                return View(new PostsDisplay
                {
                    ErrorMessage = "Sorry nothing was found with this title: ",
                    Posts = new PagedData<Post>(currentPostPage, numberOfPosts, page, PostsPerPage),
                    PostsToDisplay = currentPostPage,
                    SearchString = searchString
                });
            }           
        }

        public ActionResult CreatePost()
        {

            return View(new PostsNew
            {

            });
        }


        public ActionResult CategoryPosts(Post.CategoryName category, int page = 1)
        {
            PostDb db = new PostDb();

            var currentPostPage = db.GetPostsForCategoryPage(category, page, PostsPerPage);
            int numberOfPosts = db.NumberOfCategoryPosts(category);

            if (numberOfPosts != 0)
            {


                return View(new PostsCategory
                {

                    Posts = new PagedData<Post>(currentPostPage, numberOfPosts, page, PostsPerPage),
                    PostsToDisplay = currentPostPage,
                    NameOfCategory = category

                });
            }
            else
            {
                return View(new PostsCategory
                {
                    ErrorMessage = "Sorry nothing was found in this Category",
                    Posts = new PagedData<Post>(currentPostPage, numberOfPosts, page, PostsPerPage),
                    PostsToDisplay = currentPostPage,
                    NameOfCategory = category

                });
            }           
        }


        public ActionResult UserPosts(string name, int page = 1)
        {
            PostDb db = new PostDb();

            var currentPostPage = db.GetPostsForUsersPage(name, page, PostsPerPage);
            int numberOfPosts = db.NumberOfUsersPosts(name);


            if (numberOfPosts != 0)
            {
                return View(new PostsDisplay()
                {

                    Posts = new PagedData<Post>(currentPostPage, numberOfPosts, page, PostsPerPage),
                    PostsToDisplay = currentPostPage,
                    SearchString = name

                });
            }
            else
            {
                return View(new PostsDisplay()
                {
                    ErrorMessage = "Sorry nothing was found by this name",
                    Posts = new PagedData<Post>(currentPostPage, numberOfPosts, page, PostsPerPage),
                    PostsToDisplay = currentPostPage,
                    SearchString = name
                });
            }
        }

//-----------------------End of Controllers for displaying posts in various search ways and by categories----------------------------//

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
                Username = Session["userName"].ToString(),
                CategoryId = form.CategoryId,
                Description = form.Description,
                CreationDate = DateTime.Now
            };

            db.CreatePost(post);

            return RedirectToRoute("Home");
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
