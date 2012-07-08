using System;
using System.IO;
using System.Reflection;
using System.Web.Mvc;

namespace Uncas.NowSite.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            var assembly = GetType().Assembly;
            ViewBag.Revised = string.Format(
                "{0}, {1:G}",
                GetVersion(assembly),
                GetDate(assembly));
        }

        private static DateTime GetDate(Assembly assembly)
        {
            var fileInfo = new FileInfo(assembly.Location);
            return fileInfo.LastWriteTime;
        }

        private static string GetVersion(Assembly assembly)
        {
            var attributes = assembly.GetCustomAttributes(false);
            foreach (var attrib in attributes)
            {
                if (attrib is AssemblyInformationalVersionAttribute)
                {
                    return ((AssemblyInformationalVersionAttribute)attrib).InformationalVersion;
                }
            }

            return string.Empty;
        }
    }
}