using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Uncas.NowSite.Web.Models.Infrastructure;
using Uncas.NowSite.Web.Models.ReadModels;

namespace Uncas.NowSite.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IReadStore _readStore;

        public HomeController(IReadStore readStore)
        {
            _readStore = readStore;
        }

        public ActionResult Index()
        {
            IEnumerable<BlogPostReadModel> blogPosts
                = _readStore.GetAll<BlogPostReadModel>();
            return View(blogPosts);
        }

        public ActionResult Details(Guid id)
        {
            BlogPostReadModel blogPost =
                _readStore.GetById<BlogPostReadModel>(id);
            return View(blogPost);
        }
    }
}