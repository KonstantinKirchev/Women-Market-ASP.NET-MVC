namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using System.Net;
    using WomenMarket.Models.BindingModels;
    using WomenMarket.Models.ViewModels;
    using Services.Interfaces;

    [RouteArea("Admin")]
    [RoutePrefix("products")]
    public class ProductsController : BaseAdminController
    {
        private IProductsService service;

        public ProductsController(IWomenMarketData data, IProductsService service) 
            : base(data)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult Add()
        {
            ProductViewModel model = service.GetAddProduct();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                service.CreateNewProduct(model);

                return RedirectToAction("All", "Products", routeValues: new { area = "" });
            }

            return this.View();
        }

        [HttpGet]
        [Route("{id}/edit")]
        public ActionResult Edit(int id)
        { 
            ProductViewModel viewModel = service.GetEditProduct(id);

            return View(viewModel);
        }

        [HttpPost]
        [Route("{id}/edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                service.EditProduct(model);

                return RedirectToAction("All", "Products", routeValues: new { area = "" });
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

            ProductViewModel viewModel = service.GetDeleteProduct(id);

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
            service.DeleteProduct(id);

            return RedirectToAction("All", "Products", routeValues: new { area = "" });
        }
    }
}