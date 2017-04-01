namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using System.Net;
    using Services;
    using WomenMarket.Models.BindingModels;
    using WomenMarket.Models.ViewModels;

    [RouteArea("Admin")]
    [RoutePrefix("products")]
    public class ProductsController : BaseAdminController
    {
        private ProductsService service;

        public ProductsController(IWomenMarketData data) 
            : base(data)
        {
            service = new ProductsService(data);
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
            if (ModelState.IsValid)
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductBindingModel model)
        {
            if (ModelState.IsValid)
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
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service.DeleteProduct(id);

            return RedirectToAction("All", "Products", routeValues: new { area = "" });
        }
    }
}