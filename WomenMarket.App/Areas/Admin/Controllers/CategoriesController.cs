﻿namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using Services;
    using System.Collections.Generic;
    using WomenMarket.Models.ViewModels;
    using WomenMarket.Models.BindingModels;

    public class CategoriesController : BaseAdminController
    {
        private CategoriesService service;

        public CategoriesController(IWomenMarketData data) 
            : base(data)
        {
            service = new CategoriesService(data);
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
            if (ModelState.IsValid)
            {
                this.service.CreateNewCategory(model);
                
                return RedirectToAction("Index");
            }

            return View(model);
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
                return HttpNotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryBindingModel model)
        {
            if (ModelState.IsValid)
            {
                service.EditCategory(model);  
            }

            return RedirectToAction("Index");
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
                return HttpNotFound();
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