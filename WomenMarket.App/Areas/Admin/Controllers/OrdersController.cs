

namespace WomenMarket.App.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.UnitOfWork;
    using WomenMarket.Models;
    using System;
    using Models.ViewModels;
    using WomenMarket.Models.Enums;

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

        public PartialViewResult OrdersByStatusPartial(string status)
        {
            IEnumerable<ShoppingCart> orders;

            if (status == "All")
            {
                orders = this.Data.ShoppingCarts.All().ToList();

            }
            else
            {
                OrderStatus currentStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), status);
                orders = this.Data.ShoppingCarts.All().Where(s => s.Status == currentStatus).ToList();
            }

            IEnumerable<OrderViewModel> viewModels = Mapper.Instance.Map<IEnumerable<ShoppingCart>, IEnumerable<OrderViewModel>>(orders);

            return PartialView(viewModels);
        }

        [HttpGet]
        public ActionResult Products(int id)
        {
            var products = this.Data.ShoppingCarts.Find(id).Products.ToList();
            IEnumerable<App.Models.ViewModels.ProductViewModel> viewModels = Mapper.Instance.Map<IEnumerable<Product>, IEnumerable<App.Models.ViewModels.ProductViewModel>>(products);
            return View(viewModels);
        }

        [HttpGet]
        public ActionResult Client(int id)
        {
            var userId = this.Data.ShoppingCarts.Find(id).UserId;
            User user = this.Data.Users.Find(userId);
            return View(user);
        }
    }
}