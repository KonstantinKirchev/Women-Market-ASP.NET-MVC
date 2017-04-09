using AutoMapper;
using WomenMarket.Models.ViewModels;
using WomenMarket.Services.Interfaces;

namespace WomenMarket.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.UnitOfWork;
    using Models.EntityModels;
    using Models.Enums;

    public class ShoppingCartService : Service, IShoppingCartService
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

        //public ShoppingCartViewModel MyShoppingCart(string username)
        //{
        //    User user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

        //    ShoppingCart shoppingcart =
        //        this.Data.ShoppingCarts.All()
        //            .FirstOrDefault(s => s.UserId == user.Id && s.Status == OrderStatus.Open);

        //    ShoppingCartViewModel viewModel = null;

        //    if (shoppingcart != null)
        //    {
        //        viewModel = Mapper.Instance.Map<ShoppingCart, ShoppingCartViewModel>(shoppingcart);
        //    }

        //    return viewModel;
        //}

        public IEnumerable<ShoppingCartProductViewModel> MyShoppingCart(string username)
        {
            User user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

            IEnumerable<ShoppingCartProduct> shoppingcarts =
                this.Data.ShoppingCartProducts.All()
                    .Where(s => s.ShoppingCart.UserId == user.Id && s.ShoppingCart.Status == OrderStatus.Open).ToList();

            IEnumerable<ShoppingCartProductViewModel> viewModels = Mapper.Instance.Map<IEnumerable<ShoppingCartProduct>, IEnumerable<ShoppingCartProductViewModel>>(shoppingcarts);

            return viewModels;
        }

        public ShoppingCart GetCurrentShoppingCart(int cartId)
        {
            ShoppingCart shoppingcart = this.Data.ShoppingCarts.Find(cartId);

            return shoppingcart;
        }

        public void AddToShoppingCart(ShoppingCart cart, Product product)
        {
            var products =
                this.Data.ShoppingCartProducts.All()
                .Where(sp => sp.ShoppingCartId == cart.Id).Select(sp => sp.Product).ToList();

            ShoppingCartProduct shoppingCartProduct =
                this.Data.ShoppingCartProducts.All()
                .FirstOrDefault(sp => sp.ShoppingCartId == cart.Id && sp.ProductId == product.Id);

            if (products.Any())
            {
                foreach (var cartProduct in products)
                {
                    if (cartProduct.Id == product.Id)
                    {
                        if (shoppingCartProduct != null) shoppingCartProduct.Units++;
                        this.Data.SaveChanges();
                        return;
                    }
                }
            }
            
            var newShoppingCartProduct = new ShoppingCartProduct()
            {
                ShoppingCartId = cart.Id,
                ProductId = product.Id
            };

            this.Data.ShoppingCartProducts.Add(newShoppingCartProduct);
            this.Data.SaveChanges();
        }

        public void RemoveFromShoppingCart(ShoppingCart cart, Product product)
        {
            var shoppingCartProduct = 
                            this.Data.ShoppingCartProducts
                            .All()
                            .SingleOrDefault(mc => mc.ShoppingCartId == cart.Id && mc.ProductId == product.Id);

            if (shoppingCartProduct != null)
            {
                this.Data.ShoppingCartProducts.Remove(shoppingCartProduct);
                this.Data.SaveChanges();
            }
        }

        public void DecreaseProductUnitsFromShoppingCart(ShoppingCart cart, Product product)
        {
            var shoppingCartProduct =
                            this.Data.ShoppingCartProducts
                            .All()
                            .SingleOrDefault(mc => mc.ShoppingCartId == cart.Id && mc.ProductId == product.Id);

            if (shoppingCartProduct != null)
            {
                shoppingCartProduct.Units -= 1;

                if (shoppingCartProduct.Units == 0)
                {
                    this.Data.ShoppingCartProducts.Remove(shoppingCartProduct);
                }
                this.Data.SaveChanges();
            } 
        }

        public void IncreaseProductUnitsFromShoppingCart(ShoppingCart cart, Product product)
        {
            var shoppingCartProduct =
                            this.Data.ShoppingCartProducts
                            .All()
                            .SingleOrDefault(mc => mc.ShoppingCartId == cart.Id && mc.ProductId == product.Id);

            if (shoppingCartProduct != null)
            {
                shoppingCartProduct.Units += 1;
                this.Data.SaveChanges();
            }       
        }

        public void MakeAnOrder(int id, decimal totalAmount)
        {
            ShoppingCart cart = this.Data.ShoppingCarts.Find(id);
            cart.Status = OrderStatus.Pending;
            cart.TotalPrice = totalAmount;
            cart.DateOfOrder = DateTime.Now;

            this.Data.SaveChanges();
        }

        public bool IsProfileComplete(string username)
        {
            User user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

            if (user.Name == null || user.Address == null || user.PhoneNumber == null)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<ShoppingCartProduct> GetOrderProducts(int id)
        {
            var products =
                this.Data.ShoppingCartProducts.All()
                .Where(sp => sp.ShoppingCartId == id).ToList();

            return products;
        }
    }
}