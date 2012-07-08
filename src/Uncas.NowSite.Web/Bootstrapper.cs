using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleCqrs;
using Unity.Mvc3;
using SimpleCqrs.Eventing;
using SimpleCqrs.EventStore.File;
using System;
using System.Web;

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

        private static string GetFileEventStoreBaseDirectory()
        {
            string path = HttpContext.Current.Server.MapPath("/");
            return string.Format(
                @"{0}\..\data\NowSiteEventStore",
                path);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterFactory<IEventStore>(
                c => new FileEventStore(GetFileEventStoreBaseDirectory(),
                    c.Resolve<ITypeCatalog>()));

            //string connectionString =
            //    @"Server=.\SqlExpress;Database=SimpleCqrs;Integrated Security=true;";
            //container.Register(
            //    new SqlServerConfiguration(connectionString));
            //container.Register<IDomainEventSerializer,
            //    JsonDomainEventSerializer>();
            //container.Register<IBlogPostReadStore, BlogPostReadStore>();

            return container;
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