namespace WomenMarket.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.UnitOfWork;
    using Models.BindingModels;
    using Models.EntityModels;
    using Models.Enums;
    using Models.ViewModels;
    using Interfaces;

    public class ProfileService : Service, IProfileService
    {
        public ProfileService(IWomenMarketData data) 
            : base(data)
        {
        }

        public ProfileService(IWomenMarketData data, User user) 
            : base(data, user)
        {
        }

        public UserViewModel GetProfile(User currentUser)
        {
            UserViewModel viewModel = Mapper.Instance.Map<User, UserViewModel>(currentUser);

            return viewModel;
        }

        public IEnumerable<MyOrderViewModel> GetMyOrders(User user)
        {
            IEnumerable<ShoppingCart> shoppingcarts =
                this.Data.ShoppingCarts.All()
                    .Where(s => s.UserId == user.Id)
                    .OrderByDescending(s => s.DateOfOrder)
                    .ToList();

            IEnumerable<MyOrderViewModel> viewModels = Mapper.Instance.Map<IEnumerable<ShoppingCart>, IEnumerable<MyOrderViewModel>>(shoppingcarts);

            return viewModels;
        }

        public IEnumerable<MyOrderViewModel> GetOrdersByStatus(User user, string status)
        {
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

        public void EditUser(User user, UserBindingModel model)
        {
            user.Name = model.Name;
            user.Email = model.Email;
            user.ImageUrl = model.ImageUrl;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;

            this.Data.SaveChanges();
        }
    }
}