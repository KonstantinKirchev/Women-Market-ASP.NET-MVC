using System.Collections.Generic;
using WomenMarket.Models.BindingModels;
using WomenMarket.Models.ViewModels;

namespace WomenMarket.Services.Interfaces
{
    public interface IProfileService
    {
        UserViewModel GetProfile(string username);
        IEnumerable<MyOrderViewModel> GetMyOrders(string username);
        IEnumerable<MyOrderViewModel> GetOrdersByStatus(string username, string status);
        void EditUser(string username, UserBindingModel model);
    }
}