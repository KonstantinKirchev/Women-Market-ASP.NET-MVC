namespace WomenMarket.App.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using System.Collections.Generic;
    using WomenMarket.Models.EntityModels;
    using Services.Interfaces;
    using WomenMarket.Models.ViewModels;

    [Authorize(Roles = "User")]
    [RoutePrefix("shoppingcart")]
    public class ShoppingCartController : BaseController
    {
        private IShoppingCartService service;
        private IProfileService userService;

        public ShoppingCartController(IWomenMarketData data, IShoppingCartService service, IProfileService userService) 
            : base(data)
        {
            this.service = service;
            this.userService = userService;
        }

        public ShoppingCartController(IWomenMarketData data, User user) 
            : base(data, user)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            string username = this.User.Identity.Name;

            IEnumerable<ShoppingCartProductViewModel> carts = service.MyShoppingCart(username);

            return View(carts);
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
        [Route("cart/{cartId}/removeproduct/{id}")]
        public ActionResult RemoveProduct(int cartId, int id)
        {
            Product product = service.GetProduct(id);

            ShoppingCart cart = service.GetCurrentShoppingCart(cartId);

            service.RemoveFromShoppingCart(cart, product);

            string username = this.User.Identity.Name;

            IEnumerable<ShoppingCartProductViewModel> carts = service.MyShoppingCart(username);

            return this.PartialView("_ShoppingCartPartial", carts);
        }

        [HttpGet]
        [Route("cart/{cartId}/decreaseproductunits/{id}")]
        public ActionResult DecreaseProductUnits(int cartId, int id)
        {
            Product product = service.GetProduct(id);

            ShoppingCart cart = service.GetCurrentShoppingCart(cartId);

            service.DecreaseProductUnitsFromShoppingCart(cart, product);

            string username = this.User.Identity.Name;

            IEnumerable<ShoppingCartProductViewModel> carts = service.MyShoppingCart(username);

            return this.PartialView("_ShoppingCartPartial", carts);
        }

        [HttpGet]
        [Route("cart/{cartId}/increaseproductunits/{id}")]
        public ActionResult IncreaseProductUnits(int cartId, int id)
        {
            Product product = service.GetProduct(id);

            ShoppingCart cart = service.GetCurrentShoppingCart(cartId);

            service.IncreaseProductUnitsFromShoppingCart(cart, product);

            string username = this.User.Identity.Name;

            IEnumerable<ShoppingCartProductViewModel> carts = service.MyShoppingCart(username);

            return this.PartialView("_ShoppingCartPartial", carts);
        }

        [HttpGet]
        [Route("placeorder/{id}")]
        public ActionResult PlaceOrder(int id, decimal totalAmount)
        {
            string username = this.User.Identity.Name;

            if (!service.IsProfileComplete(username))
            {
                UserViewModel viewModel = userService.GetProfile(username);
                return this.PartialView("_EditProfilePartial", viewModel);
                //return this.RedirectToAction("Edit","Profile");

            }

            service.MakeAnOrder(id, totalAmount);

            IEnumerable<ShoppingCartProductViewModel> carts = service.MyShoppingCart(username);

            return this.PartialView("_ShoppingCartPartial", carts);
        }

        [HttpGet]
        public ActionResult Products(int id)
        {
            IEnumerable<ShoppingCartProduct> viewModels = service.GetOrderProducts(id);

            return View(viewModels);
        }
    }
}