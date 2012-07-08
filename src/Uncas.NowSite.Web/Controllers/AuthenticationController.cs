using System.Web.Mvc;
using System.Web.Security;
using Uncas.NowSite.Web.Models;

namespace Uncas.NowSite.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnInputModel model)
        {
            if (model.UserName == "test" &&
                model.Password == "test123")
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}