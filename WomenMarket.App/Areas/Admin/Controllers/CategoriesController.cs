namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using Services.Interfaces;
    using System.Collections.Generic;
    using WomenMarket.Models.ViewModels;
    using WomenMarket.Models.BindingModels;

    public class CategoriesController : BaseAdminController
    {
        private ICategoriesService service;

        public CategoriesController(IWomenMarketData data, ICategoriesService service) 
            : base(data)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<CategoryViewModel> categories = service.GetAllCategories();

            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.service.CreateNewCategory(model);
                
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryViewModel viewModel = service.GetEditCategory(id);

            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                service.EditCategory(model);

                return RedirectToAction("Index");
            }

            return this.View();

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryViewModel viewModel = service.GetDeleteCategory(id);

            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service.DeleteCategory(id);

            return RedirectToAction("Index");
        }
    }
}
