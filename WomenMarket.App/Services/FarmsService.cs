using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WomenMarket.App.Models.ViewModels;
using WomenMarket.Data.UnitOfWork;
using WomenMarket.Models;

namespace WomenMarket.App.Services
{
    public class FarmsService : Service
    {
        public FarmsService(IWomenMarketData data) : base(data)
        {
        }

        public FarmsService(IWomenMarketData data, User user) : base(data, user)
        {
        }

        public IEnumerable<FarmViewModel> GetAllFarms()
        {
            IEnumerable<Farm> farms = this.Data.Farms.All().OrderBy(f => f.Id);
            IEnumerable<FarmViewModel> viewModels = Mapper.Instance.Map<IEnumerable<Farm>, IEnumerable<FarmViewModel>>(farms);
            return viewModels;
        } 
    }
}
