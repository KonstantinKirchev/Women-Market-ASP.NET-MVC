using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WomenMarket.App.Startup))]
namespace WomenMarket.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
