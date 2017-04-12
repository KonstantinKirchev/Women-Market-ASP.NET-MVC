namespace WomenMarket.App.Controllers
{
    using System.Web.Mvc;
    using Services.Interfaces;
    using Data.UnitOfWork;
    using WomenMarket.Models.EntityModels;
    using System.Collections.Generic;
    using WomenMarket.Models.ViewModels;
    using PagedList;

    [RoutePrefix("products")]
    public class ProductsController : BaseController
    {
        private IProductsService service;

        public ProductsController(IWomenMarketData data, IProductsService service) 
            : base(data)
        {
            this.service = service;
        }

        public ProductsController(IWomenMarketData data, User user) 
            : base(data, user)
        {
        }

        [HttpGet]
        [Route("{category?}")]
        public ActionResult All(string category, int? page)
        {
            IEnumerable<ProductViewModel>  products = category == null ? this.service.GetAllProducts() : this.service.GetFilteredProducts(category);

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        [Route("search")]
        public ActionResult Search(string currentFilter, string product, int? page)
        {
            if (product != null)
            {
                page = 1;
            }
            else
            {
                product = currentFilter;

            }

            ViewBag.CurrentFilter = product;

            var products = this.service.GetSearchedProducts(product);

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            //return this.PartialView("_ProductResult", products.ToPagedList(pageNumber, pageSize));
            return this.View(products.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        [Route("farm/{farmName}")]
        public ActionResult ByFarm(string farmName, int? page)
        {
            var products = this.service.GetProductsByFarm(farmName);

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
        }
    }
}