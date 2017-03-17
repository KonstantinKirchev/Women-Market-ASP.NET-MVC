namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using Models.BindingModels;
    using WomenMarket.Models;

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
        [ValidateAntiForgeryToken]
        public ActionResult Add(FarmBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var farm = new Farm()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Address = model.Address,
                    Email = model.Email,
                    ImageUrl = model.ImageUrl,
                    PhoneNumber = model.PhoneNumber
                };

                this.Data.Farms.Add(farm);
                this.Data.SaveChanges();
            }

            return RedirectToAction("All", "Farms", routeValues: new { area = "" });
        }
    }
}