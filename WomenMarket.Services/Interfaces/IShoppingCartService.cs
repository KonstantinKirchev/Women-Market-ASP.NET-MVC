﻿namespace WomenMarket.Services.Interfaces
{
    using System.Collections.Generic;
    using Models.EntityModels;
    using Models.ViewModels;

    public interface IShoppingCartService
    {
        Product GetProduct(int id);
        ShoppingCart GetShoppingCart(User user);
        IEnumerable<ShoppingCartProductViewModel> MyShoppingCart(User user);
        ShoppingCart GetCurrentShoppingCart(int cartId);
        void AddToShoppingCart(ShoppingCart cart, Product product);
        void RemoveFromShoppingCart(ShoppingCart cart, Product product);
        void DecreaseProductUnitsFromShoppingCart(ShoppingCart cart, Product product);
        void IncreaseProductUnitsFromShoppingCart(ShoppingCart cart, Product product);
        void MakeAnOrder(int id, decimal totalAmount);
        bool IsProfileComplete(User user);
        IEnumerable<ShoppingCartProduct> GetOrderProducts(int id);
    }
}