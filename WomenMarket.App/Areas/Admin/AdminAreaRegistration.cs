using System.Web.Mvc;

namespace WomenMarket.App.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.LowercaseUrls = true;
            context.Routes.MapMvcAttributeRoutes();

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "HomeAdmin", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WomenMarket.App.Areas.Admin.Controllers" }
            );
        }
    }
}