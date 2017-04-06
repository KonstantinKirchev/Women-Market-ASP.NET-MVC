using System.Collections.Generic;
using WomenMarket.Models.EntityModels;
using WomenMarket.Models.ViewModels;

namespace WomenMarket.Services.Interfaces
{
    public interface IOrdersService
    {
        IEnumerable<OrderViewModel> GetAllOrders();
        void DeliverOrder(int id);
        IEnumerable<OrderViewModel> GetOrdersByStatus(string status);
        IEnumerable<ShoppingCartProduct> GetOrderProducts(int id);
        UserViewModel GetOrderOwner(int id);
    }
}