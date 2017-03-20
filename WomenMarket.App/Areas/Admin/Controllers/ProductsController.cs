using System.Collections.Generic;
using System.Linq;
using WomenMarket.App.Areas.Admin.Models.ViewModels;
using WomenMarket.Data;

namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Models.BindingModels;
    using Data.UnitOfWork;
    using WomenMarket.Models;

    public class ProductsController : BaseAdminController
    {
        WomenMarketContext context = new WomenMarketContext();

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
            var categories = context
                        .Categories
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name
                                });

            return new SelectList(categories, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetFarms()
        {
            var farms = context
                        .Farms
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name
                                });

            return new SelectList(farms, "Value", "Text");
        }

    }
}