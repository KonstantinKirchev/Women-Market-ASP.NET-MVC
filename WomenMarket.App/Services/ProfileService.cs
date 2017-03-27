namespace WomenMarket.App.Services
{
    using System.Linq;
    using AutoMapper;
    using Models.ViewModels;
    using Data.UnitOfWork;
    using WomenMarket.Models;
    using System;
    using System.Collections.Generic;
    using Models.BindingModels;
    using WomenMarket.Models.Enums;

    public class ProfileService : Service
    {
        public ProfileService(IWomenMarketData data) 
            : base(data)
        {
        }

        public ProfileService(IWomenMarketData data, User user) 
            : base(data, user)
        {
        }

        public UserViewModel GetProfile(string username)
        {
            User user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

            UserViewModel viewModel = Mapper.Instance.Map<User, UserViewModel>(user);

            return viewModel;
        }

        public IEnumerable<MyOrderViewModel> GetMyOrders(string username)
        {
            User user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

            IEnumerable<ShoppingCart> shoppingcarts =
                this.Data.ShoppingCarts.All()
                    .Where(s => s.UserId == user.Id).ToList();

            IEnumerable<MyOrderViewModel> viewModels = Mapper.Instance.Map<IEnumerable<ShoppingCart>, IEnumerable<MyOrderViewModel>>(shoppingcarts);

            return viewModels;
        }

        public IEnumerable<MyOrderViewModel> GetOrdersByStatus(string username, string status)
        {
            User user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

            IEnumerable<ShoppingCart> orders;

            if (status == "All")
            {
                orders = this.Data.ShoppingCarts.All().Where(s => s.UserId == user.Id).ToList();

            }
            else
            {
                OrderStatus currentStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), status);
                orders = this.Data.ShoppingCarts.All().Where(s => s.UserId == user.Id && s.Status == currentStatus).ToList();
            }

            IEnumerable<MyOrderViewModel> viewModels = Mapper.Instance.Map<IEnumerable<ShoppingCart>, IEnumerable<MyOrderViewModel>>(orders);

            return viewModels;
        }

        public void EditUser(string username, UserBindingModel model)
        {
            User user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

            user.Name = model.Name;
            user.Email = model.Email;
            user.ImageUrl = model.ImageUrl;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;

            this.Data.SaveChanges();
        }
    }
}