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
            IEnumerable<Farm> farms = this.Data.Farms.All().Where(f => f.IsDeleted == false).OrderBy(f => f.Id);
            IEnumerable<FarmViewModel> viewModels = Mapper.Instance.Map<IEnumerable<Farm>, IEnumerable<FarmViewModel>>(farms);
            return viewModels;
        }

        public void CreateNewFarm(FarmBindingModel model)
        {
            Farm existingFarm = this.Data.Farms.All().FirstOrDefault(f => f.Name == model.Name);

            if (existingFarm == null)
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
            }
            else
            {
                existingFarm.IsDeleted = false;
                existingFarm.Description = model.Description;
                existingFarm.Address = model.Address;
                existingFarm.Email = model.Email;
                existingFarm.ImageUrl = model.ImageUrl;
                existingFarm.PhoneNumber = model.PhoneNumber;
            }
            
            this.Data.SaveChanges();
        }

        public FarmViewModel GetEditFarm(int id)
        {
            Farm farm = this.Data.Farms.Find(id);

            if (farm == null || farm.IsDeleted == true)
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

            if (farm == null || farm.IsDeleted == true)
            {
                return null;
            }

            FarmViewModel viewModel = Mapper.Instance.Map<Farm, FarmViewModel>(farm);

            return viewModel;
        }

        public void DeleteFarm(int id)
        {
            Farm farm = this.Data.Farms.Find(id);
            farm.IsDeleted = true;
            this.Data.SaveChanges();
        }
    }
}
