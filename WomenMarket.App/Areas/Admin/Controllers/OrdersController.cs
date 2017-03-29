namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using WomenMarket.Models.ViewModels;
    using Services;

    public class OrdersController : BaseAdminController
    {
        private OrdersService service;

        public OrdersController(IWomenMarketData data) 
            : base(data)
        {
            service = new OrdersService(data);
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<OrderViewModel> viewModels = service.GetAllOrders();
            
            return View(viewModels);
        }

        [HttpGet]
        public ActionResult DeliverOrder(int id)
        {
            service.DeliverOrder(id);

            return this.RedirectToAction("Index","Orders");
        }

        public PartialViewResult OrdersByStatusPartial(string status)
        {
            IEnumerable<OrderViewModel> viewModels = service.GetOrdersByStatus(status);

            return PartialView(viewModels);
        }

        [HttpGet]
        public ActionResult Products(int id)
        {
            IEnumerable<ProductViewModel> viewModels = service.GetOrderProducts(id);
            
            return View(viewModels);
        }

        [HttpGet]
        public ActionResult Client(int id)
        {
            UserViewModel user = service.GetOrderOwner(id);

            return View(user);
        }
    }
}