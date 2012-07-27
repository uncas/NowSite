[assembly: WebActivator.PreApplicationStartMethod(typeof(Uncas.NowSite.Web.App_Start.ElmahMvc), "Start")]
namespace Uncas.NowSite.Web.App_Start
{
    public class ElmahMvc
    {
        public static void Start()
        {
            Elmah.Mvc.Bootstrap.Initialize();
        }
    }
}