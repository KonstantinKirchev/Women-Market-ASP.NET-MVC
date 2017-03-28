using WomenMarket.Models.EntityModels;

namespace WomenMarket.App.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using WomenMarket.Models;

    public class HomeController : BaseController
    {
        public HomeController(IWomenMarketData data) 
            : base(data)
        {
        }

        public HomeController(IWomenMarketData data, User user) 
            : base(data, user)
        {
        }

        [HttpGet]
        public ActionResult Index()
        { 
            return View();
        }

        
    }
}