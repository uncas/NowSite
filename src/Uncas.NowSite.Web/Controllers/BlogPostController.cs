using System;
using System.Web.Mvc;
using SimpleCqrs.Commanding;
using Uncas.NowSite.Web.Models.Commands;
using Uncas.NowSite.Web.Models.InputModels;

namespace Uncas.NowSite.Web.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly ICommandBus _commandBus;

        public BlogPostController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
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
            return View();
        }
    }
}