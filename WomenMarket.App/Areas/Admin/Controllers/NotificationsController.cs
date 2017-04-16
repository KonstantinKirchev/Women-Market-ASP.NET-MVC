using Microsoft.AspNet.SignalR;
using WomenMarket.App.Hubs;
using WomenMarket.Data.UnitOfWork;

namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    public class NotificationsController : BaseAdminController
    {
        public NotificationsController(IWomenMarketData data) 
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendNotification(string type, string notification)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();

            hubContext.Clients.All.receiveNotification(type, notification);

            return null;
        } 
    }
}