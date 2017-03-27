namespace WomenMarket.App.Services
{
    using Data.UnitOfWork;
    using WomenMarket.Models;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Areas.Admin.Models.ViewModels;
    using System;
    using WomenMarket.Models.Enums;

    public class OrdersService : Service
    {
        public OrdersService(IWomenMarketData data) 
            : base(data)
        {
        }

        public OrdersService(IWomenMarketData data, User user) 
            : base(data, user)
        {
        }

        public IEnumerable<OrderViewModel> GetAllOrders()
        {
            IEnumerable<ShoppingCart> orders = this.Data.ShoppingCarts.All().ToList();
            IEnumerable<OrderViewModel> viewModels = Mapper.Instance.Map<IEnumerable<ShoppingCart>, IEnumerable<OrderViewModel>>(orders);

            return viewModels;
        }

        public void DeliverOrder(int id)
        {
            var order = this.Data.ShoppingCarts.Find(id);
            order.Status = OrderStatus.Delivered;
            order.DateOfDelivery = DateTime.Now;
            this.Data.SaveChanges();
        }

        public IEnumerable<OrderViewModel> GetOrdersByStatus(string status)
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

            return viewModels;
        }

        public IEnumerable<App.Models.ViewModels.ProductViewModel> GetOrderProducts(int id)
        {
            var products = this.Data.ShoppingCarts.Find(id).Products.ToList();
            IEnumerable<App.Models.ViewModels.ProductViewModel> viewModels = Mapper.Instance.Map<IEnumerable<Product>, IEnumerable<App.Models.ViewModels.ProductViewModel>>(products);

            return viewModels;
        }

        public User GetOrderOwner(int id)
        {
            var userId = this.Data.ShoppingCarts.Find(id).UserId;
            User user = this.Data.Users.Find(userId);

            return user;
        }
    }
}