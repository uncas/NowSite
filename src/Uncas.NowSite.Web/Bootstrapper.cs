using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleCqrs;
using SimpleCqrs.Eventing;
using SimpleCqrs.EventStore.File;
using Unity.Mvc3;

namespace Uncas.NowSite.Web
{
    public class Bootstrapper
    {
        private readonly ISimpleCqrsRuntime _runtime;

        public Bootstrapper()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            _runtime = new NowSiteRuntime(container);
        }

        internal void Start()
        {
            _runtime.Start();
        }

        internal void Stop()
        {
            _runtime.Shutdown();
            _runtime.Dispose();
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterFactory<IEventStore>(
                c => new FileEventStore(GetFileEventStoreBaseDirectory(),
                    c.Resolve<ITypeCatalog>()));
            return container;
        }

        private static string GetFileEventStoreBaseDirectory()
        {
            string path = HttpContext.Current.Server.MapPath("/");
            return string.Format(
                @"{0}\..\data\NowSiteEventStore",
                path);
        }
    }

    public static class UnityExtensions
    {
        public static void RegisterFactory<TFrom>(
            this IUnityContainer container,
            Func<IUnityContainer, TFrom> factory)
        {
            container.RegisterType<TFrom>(
                new InjectionFactory(x => factory(x)));
        }
    }

}