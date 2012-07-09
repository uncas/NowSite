using System;
using System.Collections.Generic;
using System.Web.Mvc;
using StackExchange.Profiling;
using Uncas.NowSite.Web.Models.ReadStores;

namespace Uncas.NowSite.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBlogPostReadStore _blogPostReadStore;

        public HomeController(IBlogPostReadStore blogPostReadStore)
        {
            _blogPostReadStore = blogPostReadStore;
        }

        public ActionResult Index()
        {
            IEnumerable<BlogPostReadModel> blogPosts;
            var profiler = MiniProfiler.Current;
            using (profiler.Step("Get blog posts from read store"))
            {
                blogPosts = _blogPostReadStore.GetAll();
            }

            return View(blogPosts);
        }

        public ActionResult Details(Guid id)
        {
            BlogPostReadModel blogPost =
                _blogPostReadStore.GetById(id);
            return View(blogPost);
        }
    }
}