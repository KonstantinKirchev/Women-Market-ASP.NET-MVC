using WomenMarket.Services.Interfaces;

namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using WomenMarket.Models.ViewModels;
    using Services;
    using PagedList;
    using WomenMarket.Models.EntityModels;

    [RouteArea("Admin")]
    [RoutePrefix("orders")]
    public class OrdersController : BaseAdminController
    {
        private IOrdersService service;

        public OrdersController(IWomenMarketData data, IOrdersService service) 
            : base(data)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            IEnumerable<OrderViewModel> viewModels = service.GetAllOrders();

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(viewModels.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult DeliverOrder(int id)
        {
            service.DeliverOrder(id);

            return this.RedirectToAction("Index","Orders");
        }

        public PartialViewResult _OrdersByStatusPartial(string status, int? page)
        {
            IEnumerable<OrderViewModel> viewModels = service.GetOrdersByStatus(status);

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return PartialView(viewModels.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        [Route("{id}/products")]
        public ActionResult Products(int id)
        {
            IEnumerable<ShoppingCartProduct> viewModels = service.GetOrderProducts(id);
            
            return View(viewModels);
        }

        [HttpGet]
        [Route("{id}/client")]
        public ActionResult Client(int id)
        {
            UserViewModel user = service.GetOrderOwner(id);

            return View(user);
        }
    }
}