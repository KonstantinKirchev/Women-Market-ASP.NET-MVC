using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WomenMarket.Data.UnitOfWork;
using WomenMarket.Models;

namespace WomenMarket.App.Controllers
{
    [RoutePrefix("farms")]
    public class FarmsController : BaseController
    {
        public FarmsController(IWomenMarketData data) 
            : base(data)
        {
        }

        public FarmsController(IWomenMarketData data, User user) 
            : base(data, user)
        {
        }

        [HttpGet]
        [Route]
        public ActionResult All()
        {
            IEnumerable<Farm> farms = Data.Farms.All().OrderBy(f => f.Id).ToList();
            return View(farms);
        }
    }
}