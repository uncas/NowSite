using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleCqrs.Commanding;
using Uncas.NowSite.Web.Models.Commands;
using Uncas.NowSite.Web.Models.Infrastructure;
using Uncas.NowSite.Web.Models.InputModels;
using Uncas.NowSite.Web.Models.ReadModels;

namespace Uncas.NowSite.Web.Controllers
{
    [Authorize]
    public class BlogPostController : BaseController
    {
        private readonly ICommandBus _commandBus;
        private readonly IReadStore _readStore;

        public BlogPostController(
            ICommandBus commandBus,
            IReadStore readStore)
        {
            _commandBus = commandBus;
            _readStore = readStore;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<DeletedBlogPostModel> deletedPosts =
                _readStore.GetAll<DeletedBlogPostModel>();
            return View(deletedPosts);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Guid id = Guid.NewGuid();
            var create = new CreateBlogPostCommand(id);
            _commandBus.Send(create);
            return RedirectToAction("Edit", new { id });
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var startEdit = new StartEditBlogPostCommand(id);
            _commandBus.Send(startEdit);
            EditBlogPostReadModel blogPost =
                _readStore.GetById<EditBlogPostReadModel>(id);
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