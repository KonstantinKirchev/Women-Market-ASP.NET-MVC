namespace WomenMarket.App.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using WomenMarket.Models.EntityModels;
    using PagedList;
    using Services.Interfaces;

    [RoutePrefix("farms")]
    public class FarmsController : BaseController
    {
        private IFarmsService service;

        public FarmsController(IWomenMarketData data, IFarmsService service) 
            : base(data)
        {
            this.service = service;
        }

        public FarmsController(IWomenMarketData data, User user) 
            : base(data, user)
        {
        }

        [HttpGet]
        [Route]
        //[OutputCache(Duration = 60, Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult All(int? page)
        {
            var farms = this.service.GetAllFarms();

            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View(farms.ToPagedList(pageNumber, pageSize));
        }
    }
}