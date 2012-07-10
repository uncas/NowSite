using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleCqrs.Commanding;
using Uncas.NowSite.Web.Models.Commands;
using Uncas.NowSite.Web.Models.InputModels;
using Uncas.NowSite.Web.Models.ReadModels;
using Uncas.NowSite.Web.Models.ReadStores;

namespace Uncas.NowSite.Web.Controllers
{
    [Authorize]
    public class BlogPostController : BaseController
    {
        private readonly ICommandBus _commandBus;
        private readonly IBlogPostReadStore _blogPostReadStore;
        private readonly IDeletedBlogPostStore _deletedStore;
        private readonly IPictureReadStore _pictureReadStore;
        private readonly IEditBlogPostReadStore _editBlogPostReadStore;

        public BlogPostController(
            ICommandBus commandBus,
            IBlogPostReadStore blogPostReadStore,
            IDeletedBlogPostStore deletedStore,
            IEditBlogPostReadStore editBlogPostReadStore,
            IPictureReadStore pictureReadStore)
        {
            _commandBus = commandBus;
            _blogPostReadStore = blogPostReadStore;
            _deletedStore = deletedStore;
            _pictureReadStore = pictureReadStore;
            _editBlogPostReadStore = editBlogPostReadStore;
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
            return RedirectToAction("Edit", new { id });
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var createCommand = new StartEditBlogPostCommand(id);
            _commandBus.Send(createCommand);
            EditBlogPostReadModel blogPost = _editBlogPostReadStore.GetById(id);
            return View(new EditBlogPostInputModel
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Content = blogPost.Content,
                Pictures = blogPost.Pictures.Select(
                    x => new PictureInputModel
                    {
                        PictureId = x.Id,
                        PictureUrl = x.PictureUrl
                    })
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
            _commandBus.Send(new UploadPictureCommand
            {
                PictureId = pictureId,
                FileName = Path.GetFileName(file.FileName),
                FileStream = file.InputStream
            });

            _commandBus.Send(new AddPictureToBlogPostCommand
            {
                BlogPostId = blogPostId,
                PictureId = pictureId
            });

            return RedirectToAction("Edit", new { id = blogPostId });
        }
    }
}