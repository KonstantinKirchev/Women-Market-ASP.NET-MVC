namespace WomenMarket.App.Services
{
    using Data.UnitOfWork;
    using WomenMarket.Models;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Models.ViewModels;

    public class ProductsService : Service
    {
        public ProductsService(IWomenMarketData data) 
            : base(data)
        {
        }

        public ProductsService(IWomenMarketData data, User user) 
            : base(data, user)
        {
        }

        public IEnumerable<ProductViewModel> GetAllProducts()
        {
            IEnumerable<Product> products = this.Data.Products.All().OrderBy(p => p.Id);
            IEnumerable<ProductViewModel> viewModels = Mapper.Instance.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            return viewModels;
        }

        public IEnumerable<ProductViewModel> GetFilteredProducts(string category)
        {
            IEnumerable<Product> products = this.Data.Products.All().Where(p => p.Category.Name == category).OrderBy(p => p.Id);
            IEnumerable<ProductViewModel> viewModels = Mapper.Instance.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            return viewModels;
        }

        public IEnumerable<ProductViewModel> GetSearchedProducts(string product)
        {
            IEnumerable<Product> products = this.Data.Products.All().Where(p => p.Name.Contains(product)).OrderBy(p => p.Id);
            IEnumerable<ProductViewModel> viewModels = Mapper.Instance.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            return viewModels;
        }

        public IEnumerable<ProductViewModel> GetProductsByFarm(string farm)
        {
            IEnumerable<Product> products = this.Data.Products.All().Where(p => p.Owner.Name == farm).OrderBy(p => p.Id);
            IEnumerable<ProductViewModel> viewModels = Mapper.Instance.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            return viewModels;
        }
    }
}