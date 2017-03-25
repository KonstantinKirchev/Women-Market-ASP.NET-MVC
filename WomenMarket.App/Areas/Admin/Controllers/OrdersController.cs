using System;
using WomenMarket.Models.Enums;

namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Models.ViewModels;
    using Data.UnitOfWork;
    using WomenMarket.Models;

    public class OrdersController : BaseAdminController
    {
        public OrdersController(IWomenMarketData data) : base(data)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<ShoppingCart> orders = this.Data.ShoppingCarts.All().ToList();
            IEnumerable<OrderViewModel> viewModels = Mapper.Instance.Map<IEnumerable<ShoppingCart>, IEnumerable<OrderViewModel>>(orders);
            return View(viewModels);
        }

        [HttpGet]
        public ActionResult DeliverOrder(int id)
        {
            var order = this.Data.ShoppingCarts.Find(id);
            order.Status = OrderStatus.Delivered;
            order.DateOfDelivery = DateTime.Now;
            this.Data.SaveChanges();

            return this.RedirectToAction("Index","Orders");
        }
    }
}