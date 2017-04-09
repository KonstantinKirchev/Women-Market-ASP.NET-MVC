using System.Collections.Generic;
using WomenMarket.Models.EntityModels;
using WomenMarket.Models.ViewModels;

namespace WomenMarket.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Product GetProduct(int id);
        ShoppingCart GetShoppingCart(string username);
        IEnumerable<ShoppingCartProductViewModel> MyShoppingCart(string username);
        ShoppingCart GetCurrentShoppingCart(int cartId);
        void AddToShoppingCart(ShoppingCart cart, Product product);
        void RemoveFromShoppingCart(ShoppingCart cart, Product product);
        void DecreaseProductUnitsFromShoppingCart(ShoppingCart cart, Product product);
        void IncreaseProductUnitsFromShoppingCart(ShoppingCart cart, Product product);
        void MakeAnOrder(int id, decimal totalAmount);
        bool IsProfileComplete(string username);
        IEnumerable<ShoppingCartProduct> GetOrderProducts(int id);
    }
}