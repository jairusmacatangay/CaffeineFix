using System.Web.Mvc;
using CaffeineFix.Business;
using CaffeineFix.Business.Interface;
using CaffeineFix.Repository.Infrastructure;
using CaffeineFix.Repository.Infrastructure.Contract;
using Unity;
using Unity.Mvc5;

namespace CaffeineFix
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IProductsBusiness, ProductsBusiness>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}