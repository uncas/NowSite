using System;
using System.Web.Mvc;
using Uncas.NowSite.Web.Models;

namespace Uncas.NowSite.Web.Controllers
{
    public class BlogPostController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            Guid id = Guid.NewGuid();
            return View(new CreateBlogPostInputModel { Id = id });
        }

        [HttpPost]
        public ActionResult Create(CreateBlogPostInputModel model)
        {
            return View();
        }
    }
}