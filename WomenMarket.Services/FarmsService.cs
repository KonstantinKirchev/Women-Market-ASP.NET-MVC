using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WomenMarket.Data.UnitOfWork;
using WomenMarket.Models.BindingModels;
using WomenMarket.Models.EntityModels;
using WomenMarket.Models.ViewModels;

namespace WomenMarket.Services
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

        public void CreateNewFarm(FarmBindingModel model)
        {
            var farm = new Farm()
            {
                Name = model.Name,
                Description = model.Description,
                Address = model.Address,
                Email = model.Email,
                ImageUrl = model.ImageUrl,
                PhoneNumber = model.PhoneNumber
            };

            this.Data.Farms.Add(farm);
            this.Data.SaveChanges();
        }

        public FarmViewModel GetEditFarm(int id)
        {
            Farm farm = this.Data.Farms.Find(id);

            FarmViewModel viewModel = new FarmViewModel()
            {
                Id = farm.Id,
                Name = farm.Name,
                Address = farm.Address,
                PhoneNumber = farm.PhoneNumber,
                ImageUrl = farm.ImageUrl,
                Description = farm.Description,
                Email = farm.Email
            };

            return viewModel;
        }

        public void EditFarm(FarmBindingModel model)
        {
            Farm farm = this.Data.Farms.Find(model.Id);

            farm.Name = model.Name;
            farm.Description = model.Description;
            farm.ImageUrl = model.ImageUrl;
            farm.Address = model.Address;
            farm.Email = model.Email;
            farm.PhoneNumber = model.PhoneNumber;

            this.Data.SaveChanges();
        }

        public FarmViewModel GetDeeteFarm(Farm farm)
        {
            FarmViewModel viewModel = new FarmViewModel()
            {
                Id = farm.Id,
                Name = farm.Name,
                Description = farm.Description,
                Address = farm.Address,
                Email = farm.Email,
                PhoneNumber = farm.PhoneNumber,
                ImageUrl = farm.ImageUrl
            };

            return viewModel;
        }

        public void DeleteFarm(int id)
        {
            Farm farm = this.Data.Farms.Find(id);

            this.Data.Farms.Remove(farm);
            this.Data.SaveChanges();
        }
    }
}
