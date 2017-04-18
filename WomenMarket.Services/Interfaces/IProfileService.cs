namespace WomenMarket.Services.Interfaces
{
    using System.Collections.Generic;
    using Models.BindingModels;
    using Models.EntityModels;
    using Models.ViewModels;

    public interface IProfileService
    {
        UserViewModel GetProfile(User user);
        IEnumerable<MyOrderViewModel> GetMyOrders(User user);
        IEnumerable<MyOrderViewModel> GetOrdersByStatus(User user, string status);
        void EditUser(User user, UserBindingModel model);
    }
}