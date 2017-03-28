using WomenMarket.Models.EntityModels;

namespace WomenMarket.App.Controllers
{
    using System.Web.Mvc;
    using Services;
    using Data.UnitOfWork;
    using WomenMarket.Models;

    [RoutePrefix("products")]
    public class ProductsController : BaseController
    {
        private ProductsService service;

        public ProductsController(IWomenMarketData data) : base(data)
        {
            this.service = new ProductsService(data);
        }

        public ProductsController(IWomenMarketData data, User user) : base(data, user)
        {
        }

        [HttpGet]
        [Route]
        public ActionResult All()
        {
            var products = this.service.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        [Route("{category}")]
        public ActionResult All(string category)
        {
            var products = this.service.GetFilteredProducts(category);
            return View(products);
        }

        [HttpGet]
        [Route("search")]
        public ActionResult Search(string product)
        {
            var products = this.service.GetSearchedProducts(product);
            return View(products);
        }

        [HttpGet]
        [Route("farm/{farmName}")]
        public ActionResult ByFarm(string farmName)
        {
            var products = this.service.GetProductsByFarm(farmName);
            return View(products);
        }
    }
}