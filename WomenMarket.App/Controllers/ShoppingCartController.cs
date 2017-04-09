using WomenMarket.Models.ViewModels;

namespace WomenMarket.App.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using System.Collections.Generic;
    using WomenMarket.Models.EntityModels;
    using Services.Interfaces;

    [RoutePrefix("shoppingcart")]
    public class ShoppingCartController : BaseController
    {
        private IShoppingCartService service;

        public ShoppingCartController(IWomenMarketData data, IShoppingCartService service) 
            : base(data)
        {
            this.service = service;
        }

        public ShoppingCartController(IWomenMarketData data, User user) 
            : base(data, user)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            string username = this.User.Identity.Name;

            IEnumerable<ShoppingCartProductViewModel> cart = service.MyShoppingCart(username);

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
            IEnumerable<ShoppingCartProduct> viewModels = service.GetOrderProducts(id);

            return View(viewModels);
        }
    }
}