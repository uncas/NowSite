using Microsoft.Practices.Unity;
using SimpleCqrs;
using SimpleCqrs.Eventing;

namespace Uncas.NowSite.Web
{
    public class NowSiteRuntime :
        SimpleCqrsRuntime<SimpleCqrs.Unity.UnityServiceLocator>
    {
        private readonly IUnityContainer _container;

        public NowSiteRuntime(IUnityContainer container)
        {
            _container = container;
        }

        protected override SimpleCqrs.Unity.UnityServiceLocator
            GetServiceLocator()
        {
            return new SimpleCqrs.Unity.UnityServiceLocator(_container);
        }

        protected override IEventStore GetEventStore(
            IServiceLocator serviceLocator)
        {
            return serviceLocator.Resolve<IEventStore>();
        }
    }
}