using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleCqrs;
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

            //string connectionString =
            //    @"Server=.\SqlExpress;Database=SimpleCqrs;Integrated Security=true;";
            //container.Register(
            //    new SqlServerConfiguration(connectionString));
            //container.Register<IEventStore, SqlServerEventStore>();
            //container.Register<IDomainEventSerializer,
            //    JsonDomainEventSerializer>();
            //container.Register<IBlogPostReadStore, BlogPostReadStore>();

            return container;
        }
    }
}