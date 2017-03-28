using System.Collections.Generic;
using WomenMarket.App.Models.ViewModels;
using WomenMarket.App.Services;
using WomenMarket.Models.EntityModels;
using WomenMarket.Models.Enums;
using WomenMarket.Models.ViewModels;

namespace WomenMarket.App.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using WomenMarket.Models;

    [RoutePrefix("shoppingcart")]
    public class ShoppingCartController : BaseController
    {
        private ShoppingCartService service;

        public ShoppingCartController(IWomenMarketData data) 
            : base(data)
        {
            service = new ShoppingCartService(data);
        }

        public ShoppingCartController(IWomenMarketData data, User user) 
            : base(data, user)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            string username = this.User.Identity.Name;

            ShoppingCartViewModel cart = service.MyShoppingCart(username);

            return View(cart);
        }

        [HttpGet]
        [Route("addproduct/{id}")]
        public ActionResult AddProduct(int id)
        {
            Product product = service.GetProduct(id);

            string username = this.User.Identity.Name;

            ShoppingCart cart = service.GetShoppingCart(username);

            service.AddToShoppingCart(cart, product);

            return this.RedirectToAction("All", "Products");
        }

        [HttpGet]
        [Route("removeproduct/{id}")]
        public ActionResult RemoveProduct(int cartId, int id)
        {
            Product product = service.GetProduct(id);

            ShoppingCart cart = service.GetCurrentShoppingCart(cartId);

            service.RemoveFromShoppingCart(cart, product);

            return this.RedirectToAction("Index", "ShoppingCart");
        }

        [HttpGet]
        [Route("decreaseproductunits/{id}")]
        public ActionResult DecreaseProductUnits(int cartId, int id)
        {
            Product product = service.GetProduct(id);

            ShoppingCart cart = service.GetCurrentShoppingCart(cartId);

            service.DecreaseProductUnitsFromShoppingCart(cart, product);

            return this.RedirectToAction("Index", "ShoppingCart");
        }

        [HttpGet]
        [Route("increaseproductunits/{id}")]
        public ActionResult IncreaseProductUnits(int cartId, int id)
        {
            Product product = service.GetProduct(id);

            ShoppingCart cart = service.GetCurrentShoppingCart(cartId);

            service.IncreaseProductUnitsFromShoppingCart(cart, product);

            return this.RedirectToAction("Index", "ShoppingCart");
        }

        [HttpGet]
        [Route("placeorder/{id}")]
        public ActionResult PlaceOrder(int id, decimal totalAmount)
        {
            string username = this.User.Identity.Name;

            if (!service.IsProfileComplete(username))
            {
                return this.RedirectToAction("Edit","Profile");
            }

            service.MakeAnOrder(id, totalAmount);

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Products(int id)
        {
            IEnumerable<ProductViewModel> viewModels = service.GetOrderProducts(id);

            return View(viewModels);
        }
    }
}