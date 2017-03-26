using System;

namespace WomenMarket.App.Services
{
    using Data.UnitOfWork;
    using WomenMarket.Models;
    using System.Linq;
    using AutoMapper;
    using Models.ViewModels;
    using WomenMarket.Models.Enums;

    public class ShoppingCartService : Service
    {
        public ShoppingCartService(IWomenMarketData data) 
            : base(data)
        {
        }

        public ShoppingCartService(IWomenMarketData data, User user) 
            : base(data, user)
        {
        }

        public Product GetProduct(int id)
        {
            var product = this.Data.Products.Find(id);
            return product;
        }

        public ShoppingCart GetShoppingCart(string username)
        {
            User user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

            ShoppingCart shoppingcart =
                this.Data.ShoppingCarts.All()
                    .FirstOrDefault(s => s.UserId == user.Id && s.Status == OrderStatus.Open);

            if (shoppingcart == null)
            {
                shoppingcart = new ShoppingCart()
                { 
                    UserId = user.Id,
                    TotalPrice = 0.00m
                };

                this.Data.ShoppingCarts.Add(shoppingcart);
                this.Data.SaveChanges();
            }

            return shoppingcart;
        }

        public ShoppingCartViewModel MyShoppingCart(string username)
        {
            User user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

            ShoppingCart shoppingcart =
                this.Data.ShoppingCarts.All()
                    .FirstOrDefault(s => s.UserId == user.Id && s.Status == OrderStatus.Open);

            ShoppingCartViewModel viewModel = null;

            if (shoppingcart != null)
            {
                viewModel = Mapper.Instance.Map<ShoppingCart, ShoppingCartViewModel>(shoppingcart);
            }

            return viewModel;
        }

        public ShoppingCart GetCurrentShoppingCart(int cartId)
        {
            ShoppingCart shoppingcart = this.Data.ShoppingCarts.Find(cartId);

            return shoppingcart;
        }

        public void AddToShoppingCart(ShoppingCart cart, Product product)
        {
            cart.Products.Add(product);
            this.Data.SaveChanges();
        }

        public void RemoveFromShoppingCart(ShoppingCart cart, Product product)
        {
            product.Units = 1;
            cart.Products.Remove(product);
            this.Data.SaveChanges();
        }

        public void DecreaseProductUnitsFromShoppingCart(ShoppingCart cart, Product product)
        {
            product.Units -= 1;

            if (product.Units == 0)
            {
                cart.Products.Remove(product);
                product.Units = 1;
            }

            this.Data.SaveChanges();
        }

        public void IncreaseProductUnitsFromShoppingCart(ShoppingCart cart, Product product)
        {
            product.Units += 1;
            this.Data.SaveChanges();
        }

        public void MakeAnOrder(int id, decimal totalAmount)
        {
            ShoppingCart cart = this.Data.ShoppingCarts.Find(id);
            cart.Status = OrderStatus.Pending;
            cart.TotalPrice = totalAmount;
            cart.DateOfOrder = DateTime.Now;
            var products = this.Data.Products.All().ToList();
            foreach (var product in products)
            {
                product.Units = 1;
            }
            this.Data.SaveChanges();
        }
    }
}