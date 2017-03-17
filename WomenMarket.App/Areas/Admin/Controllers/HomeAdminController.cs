using WomenMarket.Data.UnitOfWork;

namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    public class HomeAdminController : BaseAdminController
    {
        public HomeAdminController(IWomenMarketData data) : base(data)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        
    }
}