using WomenMarket.Models.BindingModels;
using WomenMarket.Models.EntityModels;
using WomenMarket.Models.ViewModels;

namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using WomenMarket.Models;
    using System.Net;
    using App.Models.ViewModels;
    using Services;

    public class FarmsController : BaseAdminController
    {
        private FarmsService service;

        public FarmsController(IWomenMarketData data) 
            : base(data)
        {
            service = new FarmsService(data);
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
                service.CreateNewFarm(model);
            }

            return RedirectToAction("All", "Farms", routeValues: new { area = "" });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            FarmViewModel viewModel = service.GetEditFarm(id);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FarmBindingModel model)
        {
            if (ModelState.IsValid)
            {
                service.EditFarm(model);
            }

            return RedirectToAction("All", "Farms", routeValues: new { area = "" });
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Farm farm = this.Data.Farms.Find(id);

            if (farm == null)
            {
                return HttpNotFound();
            }

            FarmViewModel viewModel = service.GetDeeteFarm(farm);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service.DeleteFarm(id);
           
            return RedirectToAction("All", "Farms", routeValues: new { area = "" });
        }
    }
}