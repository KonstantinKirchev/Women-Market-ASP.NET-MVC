namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Models.BindingModels;
    using Data.UnitOfWork;
    using WomenMarket.Models;
    using System.Collections.Generic;
    using System.Linq;
    using Models.ViewModels;
    using System.Net;

    public class ProductsController : BaseAdminController
    {
        public ProductsController(IWomenMarketData data) 
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Add()
        {
            var model = new ProductViewModel()
            {
                Categories = GetCategories(),
                Farms = GetFarms()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    ImageUrl = model.ImageUrl,
                    Unites = 1,
                    CategoryId = model.CategoryId,
                    OwnerId = model.FarmId
                };

                this.Data.Products.Add(product);
                this.Data.SaveChanges();
            }

            return RedirectToAction("All", "Products", routeValues: new { area = "" });
        }

        private IEnumerable<SelectListItem> GetCategories()
        {
            var categories = this.Data
                        .Categories
                        .All()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name
                                });

            return new SelectList(categories, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetCategoriesById(int id)
        {
            //var productName = Data.Products.Find(id).Name;

            var categories = this.Data
                        .Categories
                        .All()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name,
                                    Selected = x.Id == id
                                });

            return new SelectList(categories, "Value", "Text", id);
        }

        private IEnumerable<SelectListItem> GetFarms()
        {
            var farms = this.Data
                        .Farms
                        .All()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name
                                });

            return new SelectList(farms, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetFarmsById(int id)
        {
            var farms = this.Data
                        .Farms
                        .All()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name,
                                    Selected = x.Id == id
                                });

            return new SelectList(farms, "Value", "Text", id);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product product = this.Data.Products.Find(id);

            ProductViewModel viewModel = new ProductViewModel()
            {
                Id = product.Id,
                Categories = GetCategoriesById(product.CategoryId),
                Farms = GetFarmsById(product.OwnerId),
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var product = Data.Products.Find(model.Id);
                product.Name = model.Name;
                product.Description = model.Description;
                product.ImageUrl = model.ImageUrl;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                product.CategoryId = model.CategoryId;
                product.OwnerId = model.FarmId;
                
                this.Data.SaveChanges();
            }

            return RedirectToAction("All", "Products", routeValues: new { area = "" });
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = this.Data.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            ProductViewModel viewModel = new ProductViewModel()
            {
                Id = product.Id,
                Categories = GetCategoriesById(product.CategoryId),
                Farms = GetFarmsById(product.OwnerId),
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = this.Data.Products.Find(id);
            this.Data.Products.Remove(product);
            this.Data.SaveChanges();

            return RedirectToAction("All", "Products", routeValues: new { area = "" });
        }
    }
}