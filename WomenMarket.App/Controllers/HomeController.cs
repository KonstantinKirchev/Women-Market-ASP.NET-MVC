using System.Web.Mvc;
using WomenMarket.Data.UnitOfWork;
using WomenMarket.Models;

namespace WomenMarket.App.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IWomenMarketData data) : base(data)
        {
        }

        public HomeController(IWomenMarketData data, User user) : base(data, user)
        {
        }

        public ActionResult Index()
        { 
            return View();
        }

        
    }
}