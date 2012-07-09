using System.Web.Mvc;
using System.Web.Routing;
using StackExchange.Profiling;
using StackExchange.Profiling.MVCHelpers;

namespace Uncas.NowSite.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private Bootstrapper _bootstrapper;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            _bootstrapper = new Bootstrapper();
            _bootstrapper.Start();
            GlobalFilters.Filters.Add(new ProfilingActionFilter());
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal || Request.IsAuthenticated)
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_End()
        {
            if (_bootstrapper != null)
                _bootstrapper.Stop();
        }
    }
}