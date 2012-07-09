using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.GData.Photos;
using SimpleCqrs.Commanding;
using Uncas.NowSite.Web.Models.Commands;
using Uncas.NowSite.Web.Models.InputModels;
using Uncas.NowSite.Web.Models.ReadStores;

namespace Uncas.NowSite.Web.Controllers
{
    [Authorize]
    public class BlogPostController : BaseController
    {
        private readonly ICommandBus _commandBus;

        private readonly IBlogPostReadStore _blogPostReadStore;
        private readonly IDeletedBlogPostStore _deletedStore;

        public BlogPostController(
            ICommandBus commandBus,
            IBlogPostReadStore blogPostReadStore,
            IDeletedBlogPostStore deletedStore)
        {
            _commandBus = commandBus;
            _blogPostReadStore = blogPostReadStore;
            _deletedStore = deletedStore;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<DeletedBlogPostModel> deletedPosts =
                _deletedStore.GetAll();
            return View(deletedPosts);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Guid id = Guid.NewGuid();
            var createCommand = new CreateBlogPostCommand(id);
            _commandBus.Send(createCommand);
            return View(new CreateBlogPostInputModel { Id = id });
        }

        [HttpPost]
        public ActionResult Create(CreateBlogPostInputModel model)
        {
            Guid id = model.Id;
            var addInfoCommand = new AddBlogPostInfoCommand(
                id,
                model.Title,
                model.Content);
            _commandBus.Send(addInfoCommand);
            var publishCommand = new PublishBlogPostCommand(id);
            _commandBus.Send(publishCommand);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var createCommand = new StartEditBlogPostCommand(id);
            _commandBus.Send(createCommand);
            BlogPostReadModel blogPost = _blogPostReadStore.GetById(id);
            return View(new EditBlogPostInputModel
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Content = blogPost.Content
            });
        }

        [HttpPost]
        public ActionResult Edit(EditBlogPostInputModel model)
        {
            Guid id = model.Id;
            var addInfoCommand = new AddBlogPostInfoCommand(
               id,
               model.Title,
               model.Content);
            _commandBus.Send(addInfoCommand);
            var publishCommand = new PublishBlogPostCommand(id);
            _commandBus.Send(publishCommand);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var deleteCommand = new DeleteBlogPostCommand(id);
            _commandBus.Send(deleteCommand);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Publish(Guid id)
        {
            var publishCommand = new PublishBlogPostCommand(id);
            _commandBus.Send(publishCommand);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Replay()
        {
            _commandBus.Send(new SyncBlogPostsCommand());
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UploadPicture(
            Guid blogPostId,
            HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                return Content("No file");
            }

            Guid pictureId = Guid.NewGuid();
            string fileName = Path.GetFileName(file.FileName);

            // TODO: Read bytes from stream and use them in command:
            Stream fileStream = file.InputStream;

            // TODO: Use command:
            string photoUrl = UploadPicture(
                blogPostId, 
                pictureId, 
                fileName, 
                fileStream);

            // TODO: Get URL from read query:
            return Redirect(photoUrl);
        }

        private string UploadPicture(
            Guid blogPostId,
            Guid pictureId,
            string fileName,
            Stream fileStream)
        {
            PicasaService service = new PicasaService("NowSite");
            string userName = "username@gmail.com";
            service.setUserCredentials(
                userName,
                "pwd");

            string albumTitle = "NowSite";
            if (!DoesAlbumExist(service, albumTitle))
            {
                CreateAlbum(service, albumTitle);
            }

            string albumId = GetAlbumId(service, albumTitle);

            return AddPhoto(
                service,
                albumId,
                fileName,
                fileStream);
        }

        private string GetAlbumId(
            PicasaService service,
            string albumTitle)
        {
            AlbumQuery query = new AlbumQuery(
                           PicasaQuery.CreatePicasaUri("default"));
            PicasaFeed feed = service.Query(query);
            foreach (PicasaEntry entry in feed.Entries)
            {
                if (albumTitle.Equals(entry.Title.Text))
                {
                    return entry.Id.AbsoluteUri.Split('/').Last();
                }
            }

            return null;
        }

        private bool DoesAlbumExist(
            PicasaService service,
            string albumName)
        {
            AlbumQuery query = new AlbumQuery(
                           PicasaQuery.CreatePicasaUri("default"));
            PicasaFeed feed = service.Query(query);
            foreach (PicasaEntry entry in feed.Entries)
            {
                if (albumName.Equals(entry.Title.Text))
                {
                    return true;
                }
            }

            return false;
        }

        private void CreateAlbum(PicasaService service, string albumTitle)
        {
            var newEntry = new AlbumEntry();
            newEntry.Title.Text = albumTitle;
            newEntry.Summary.Text = "Album used for pictures for NowSite";
            var ac = new AlbumAccessor(newEntry);
            //set to "private" for a private album
            ac.Access = "public";
            var feedUri = new Uri(PicasaQuery.CreatePicasaUri("default"));
            var createdEntry = (PicasaEntry)service.Insert(feedUri, newEntry);
        }

        private string AddPhoto(
            PicasaService service,
            string albumId,
            string fileName,
            Stream fileStream)
        {
            var postUri = new Uri(
                PicasaQuery.CreatePicasaUri("default", albumId));
            var entry = (PicasaEntry)service.Insert(
                postUri,
                fileStream,
                "image/jpeg",
                fileName);
            fileStream.Close();
            return entry.Media.Content.Url;
        }
    }
}