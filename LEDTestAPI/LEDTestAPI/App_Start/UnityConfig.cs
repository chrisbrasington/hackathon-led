using System.Web.Http;
using LEDTestAPI.Services;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.WebApi;

namespace LEDTestAPI
{
    public static class UnityConfig
   {
      public static void RegisterComponents()
      {
         var container = new UnityContainer();

         container.RegisterType<SerialService>(new PerResolveLifetimeManager(),
            new InjectionFactory(c => new SerialService()));

         GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
      }
   }
}