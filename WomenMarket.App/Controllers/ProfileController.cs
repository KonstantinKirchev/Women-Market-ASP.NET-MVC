using System;
using System.Collections.Generic;
using WomenMarket.Models.Enums;

namespace WomenMarket.App.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using AutoMapper;
    using Models.BindingModels;
    using Models.ViewModels;
    using Data.UnitOfWork;
    using WomenMarket.Models;

    public class ProfileController : BaseController
    {
        public ProfileController(IWomenMarketData data) 
            : base(data)
        {
        }

        public ProfileController(IWomenMarketData data, User user) 
            : base(data, user)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            User user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
            UserViewModel viewModel = Mapper.Instance.Map<User, UserViewModel>(user);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var username = User.Identity.Name;
            User user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
            UserViewModel viewModels = Mapper.Instance.Map<User, UserViewModel>(user);
            return View(viewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                User user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

                user.Name = model.Name;
                user.Email = model.Email;
                user.ImageUrl = model.ImageUrl;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;

                this.Data.SaveChanges();
            }

            return RedirectToAction("Index", "Profile", routeValues: new { area = "" });
        }

        [HttpGet]
        public ActionResult MyOrders()
        {
            var username = User.Identity.Name;
            User user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

            IEnumerable<ShoppingCart> shoppingcarts =
                this.Data.ShoppingCarts.All()
                    .Where(s => s.UserId == user.Id).ToList();

            IEnumerable<MyOrderViewModel> viewModels = Mapper.Instance.Map<IEnumerable<ShoppingCart>, IEnumerable<MyOrderViewModel>>(shoppingcarts);

            return View(viewModels);
        }

        public PartialViewResult OrdersByStatusPartial(string status)
        {
            var username = User.Identity.Name;
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

            return PartialView(viewModels);
        }
    }
}