using WomenMarket.Services.Interfaces;

namespace WomenMarket.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.UnitOfWork;
    using Models.EntityModels;
    using Models.Enums;
    using Models.ViewModels;

    public class OrdersService : Service, IOrdersService
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

        public IEnumerable<ShoppingCartProduct> GetOrderProducts(int id)
        {
            var products =
                this.Data.ShoppingCartProducts.All()
                .Where(sp => sp.ShoppingCartId == id).ToList();

            return products;
        }

        public UserViewModel GetOrderOwner(int id)
        {
            var userId = this.Data.ShoppingCarts.Find(id).UserId;
            User user = this.Data.Users.Find(userId);
            UserViewModel viewModel = Mapper.Instance.Map<User, UserViewModel>(user);

            return viewModel;
        }
    }
}