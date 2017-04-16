using Microsoft.AspNet.SignalR.Hubs;

namespace WomenMarket.App.Hubs
{
    using Microsoft.AspNet.SignalR;

    [HubName("notifications")]
    public class NotificationsHub : Hub
    {
        public void SendNotification(string type, string notification)
        {
            this.Clients.Others.receiveNotification(type, notification);
        }
    }
}