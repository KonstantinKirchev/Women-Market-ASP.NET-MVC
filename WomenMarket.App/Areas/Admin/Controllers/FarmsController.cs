using WomenMarket.Services.Interfaces;

namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using System.Net;
    using Services;
    using WomenMarket.Models.BindingModels;
    using WomenMarket.Models.ViewModels;

    [RouteArea("Admin")]
    [RoutePrefix("farms")]
    public class FarmsController : BaseAdminController
    {
        private IFarmsService service;

        public FarmsController(IWomenMarketData data, IFarmsService service) 
            : base(data)
        {
            this.service = service;
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

                return RedirectToAction("All", "Farms", routeValues: new { area = "" });
            }

            return this.View();
        }

        [HttpGet]
        [Route("{id}/edit")]
        public ActionResult Edit(int id)
        {
            FarmViewModel viewModel = service.GetEditFarm(id);

            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        [Route("{id}/edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FarmBindingModel model)
        {
            if (ModelState.IsValid)
            {
                service.EditFarm(model);

                return RedirectToAction("All", "Farms", routeValues: new { area = "" });
            }

            return this.View();
        }

        [HttpGet]
        [Route("{id}/delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FarmViewModel viewModel = service.GetDeleteFarm(id);

            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Route("{id}/delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service.DeleteFarm(id);
           
            return RedirectToAction("All", "Farms", routeValues: new { area = "" });
        }
    }
}