using WomenMarket.App.Areas.Admin.Models.BindingModels;

namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;

    public class FarmsController : BaseAdminController
    {
        public FarmsController(IWomenMarketData data) 
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(FarmBindingModel model)
        {
            return View();
        }
    }
}