using System.Web.Mvc;

namespace Uncas.NowSite.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Changes()
        {
            return View();
        }

        [OutputCache(Duration = 600)]
        public ActionResult CommitHistory()
        {
            return PartialView();
        }
    }
}