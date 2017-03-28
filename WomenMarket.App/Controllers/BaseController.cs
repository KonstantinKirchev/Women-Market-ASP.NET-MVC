namespace WomenMarket.App.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using System.Web.Routing;
    using Data.UnitOfWork;
    using WomenMarket.Models.EntityModels;

    public class BaseController : Controller
    {
        public BaseController(IWomenMarketData data)
        {
            this.Data = data;
        }

        public BaseController(IWomenMarketData data, User user)
            : this(data)
        {
            this.UserProfile = user;
        }

        public IWomenMarketData Data { get; private set; }

        public User UserProfile { get; set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
                this.UserProfile = user;
            }
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}