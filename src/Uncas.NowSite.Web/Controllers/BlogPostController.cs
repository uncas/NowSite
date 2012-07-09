using System;
using System.Collections.Generic;
using System.Web.Mvc;
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
    }
}