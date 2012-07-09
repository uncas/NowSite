using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleCqrs;
using SimpleCqrs.Eventing;
using SimpleCqrs.Utilites;
using Uncas.NowSite.Web.Models.ReadStores;
using Uncas.NowSite.Web.Utilities;
using Unity.Mvc3;

namespace Uncas.NowSite.Web
{
    public class Bootstrapper
    {
        private readonly ISimpleCqrsRuntime _runtime;
        private readonly IUnityContainer _container;

        public Bootstrapper()
        {
            _container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(_container));
            _runtime = new NowSiteRuntime(_container);
            _container.RegisterInstance(
                typeof(DomainEventReplayer),
                new DomainEventReplayer(_runtime));
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
                c => new NowFileEventStore(GetDataDirectory("EventStore"),
                    c.Resolve<ITypeCatalog>()));
            container.RegisterType<IStringSerializer, JsonStringSerializer>();
            container.RegisterFactory<IBlogPostReadStore>(
                c => new BlogPostReadStore(
                    GetDataDirectory("ReadStore.db"),
                    c.Resolve<IStringSerializer>()));
            container.RegisterFactory<IBlogPostMasterStore>(
                c => new BlogPostMasterStore(
                    GetDataDirectory("ReadStore.db"),
                    c.Resolve<IStringSerializer>()));
            container.RegisterFactory<IDeletedBlogPostStore>(
                c => new DeletedBlogPostStore(
                    GetDataDirectory("ReadStore.db"),
                    c.Resolve<IStringSerializer>()));
            return container;
        }

        private static string GetDataDirectory(string dataType)
        {
            return string.Format(
                @"{0}\{1}",
                GetBaseDataDirectory(),
                dataType);
        }

        private static string GetBaseDataDirectory()
        {
            string path = HttpContext.Current.Server.MapPath("/");
            return string.Format(
                @"{0}\..\data\NowSite",
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