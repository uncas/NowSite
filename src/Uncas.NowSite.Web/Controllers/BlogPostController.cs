using System;
using System.Web.Mvc;
using SimpleCqrs.Commanding;
using Uncas.NowSite.Web.Models.Commands;
using Uncas.NowSite.Web.Models.InputModels;
using Uncas.NowSite.Web.Models.ReadStores;
using System.Collections.Generic;

namespace Uncas.NowSite.Web.Controllers
{
    [Authorize]
    public class BlogPostController : BaseController
    {
        private readonly ICommandBus _commandBus;

        private readonly IBlogPostReadStore _blogPostReadStore;
        private readonly IBlogPostMasterStore _masterStore;

        public BlogPostController(
            ICommandBus commandBus,
            IBlogPostReadStore blogPostReadStore,
            IBlogPostMasterStore masterStore)
        {
            _commandBus = commandBus;
            _blogPostReadStore = blogPostReadStore;
            _masterStore = masterStore;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<BlogPostMasterModel> blogPosts =
                _masterStore.GetAll();
            return View(blogPosts);
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
    }
}