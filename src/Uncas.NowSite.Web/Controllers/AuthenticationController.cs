using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Uncas.NowSite.Web.Models.InputModels;

namespace Uncas.NowSite.Web.Controllers
{
    public class AuthenticationController : BaseController
    {
        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnInputModel model)
        {
            if (model.UserName == ConfigurationManager.AppSettings["TestUser.UserName"] &&
                model.Password == ConfigurationManager.AppSettings["TestUser.Password"])
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

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