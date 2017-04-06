using System.Collections.Generic;
using WomenMarket.Models.BindingModels;
using WomenMarket.Models.ViewModels;

namespace WomenMarket.Services.Interfaces
{
    public interface IFarmsService
    {
        IEnumerable<FarmViewModel> GetAllFarms();
        void CreateNewFarm(FarmBindingModel model);
        FarmViewModel GetEditFarm(int id);
        void EditFarm(FarmBindingModel model);
        FarmViewModel GetDeleteFarm(int? id);
        void DeleteFarm(int id);
    }
}