using System.Collections.Generic;
using System.Web.Mvc;
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
            IEnumerable<BlogPostReadModel> blogPosts =
                _blogPostReadStore.GetBlogPosts();
            return View(blogPosts);
        }
    }
}