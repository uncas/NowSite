using System;
using Microsoft.Practices.Unity;

namespace Uncas.NowSite.Web.Utilities
{
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