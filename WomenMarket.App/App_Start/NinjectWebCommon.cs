using System;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using WomenMarket.App;
using WomenMarket.Data;
using WomenMarket.Data.Interfaces;
using WomenMarket.Data.UnitOfWork;
using WomenMarket.Models.EntityModels;
using WomenMarket.Services;
using WomenMarket.Services.Interfaces;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace WomenMarket.App
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IWomenMarketData>().To<WomenMarketData>()
               .InRequestScope()
               .WithConstructorArgument("context", p => new WomenMarketContext());

            kernel.Bind<IUserStore<User>>().To<UserStore<User>>()
                    .InRequestScope()
                    .WithConstructorArgument("context", kernel.Get<WomenMarketContext>());

            kernel.Bind<IAuthenticationManager>()
                    .ToMethod<IAuthenticationManager>(context => HttpContext.Current.GetOwinContext().Authentication)
                    .InRequestScope();

            kernel.Bind<IFarmsService>().To<FarmsService>().InRequestScope();
            kernel.Bind<ICategoriesService>().To<CategoriesService>().InRequestScope();
            kernel.Bind<IProductsService>().To<ProductsService>().InRequestScope();
            kernel.Bind<IOrdersService>().To<OrdersService>().InRequestScope();
            kernel.Bind<IShoppingCartService>().To<ShoppingCartService>().InRequestScope();
            kernel.Bind<IProfileService>().To<ProfileService>().InRequestScope();

        }
    }
}
