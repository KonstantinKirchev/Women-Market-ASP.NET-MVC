using WomenMarket.App.Models.ViewModels;
using WomenMarket.App.Services;

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
    }
}