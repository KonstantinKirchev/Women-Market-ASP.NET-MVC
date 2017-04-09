namespace WomenMarket.App
{
    using System.Data.Entity;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Data;
    using Data.Migrations;
    using AutoMapper;
    using WomenMarket.Models.EntityModels;
    using WomenMarket.Models.ViewModels;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.RegisterMaps();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WomenMarketContext, Configuration>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterMaps()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Farm, FarmViewModel>();
                expression.CreateMap<Product, ProductViewModel>();
                expression.CreateMap<Category, CategoryViewModel>();
                expression.CreateMap<ShoppingCart, ShoppingCartViewModel>();
                expression.CreateMap<ShoppingCartProduct, ShoppingCartProductViewModel>();
                expression.CreateMap<ShoppingCart, OrderViewModel>();
                expression.CreateMap<ShoppingCart, MyOrderViewModel>();
                expression.CreateMap<User, UserViewModel>();
            });
        }
    }
}
