namespace WomenMarket.App.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using WomenMarket.Models.EntityModels;
    using Services;
    using PagedList;

    [RoutePrefix("farms")]
    public class FarmsController : BaseController
    {
        private FarmsService service;

        public FarmsController(IWomenMarketData data) 
            : base(data)
        {
            this.service = new FarmsService(data);
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