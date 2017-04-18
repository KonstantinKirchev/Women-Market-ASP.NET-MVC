namespace WomenMarket.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.UnitOfWork;
    using Models.BindingModels;
    using Models.EntityModels;
    using Models.ViewModels;
    using Interfaces;

    public class FarmsService : Service, IFarmsService
    {
        public FarmsService(IWomenMarketData data) 
            : base(data)
        {
        }

        public FarmsService(IWomenMarketData data, User user) 
            : base(data, user)
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

            if (farm == null)
            {
                return null;
            }

            FarmViewModel viewModel = Mapper.Instance.Map<Farm, FarmViewModel>(farm);

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

        public FarmViewModel GetDeleteFarm(int? id)
        {
            Farm farm = this.Data.Farms.Find(id);

            if (farm == null)
            {
                return null;
            }

            FarmViewModel viewModel = Mapper.Instance.Map<Farm, FarmViewModel>(farm);

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
