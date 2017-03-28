namespace WomenMarket.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.UnitOfWork;
    using Models.BindingModels;
    using Models.EntityModels;
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

        public ProductViewModel GetAddProduct()
        {
            ProductViewModel model = new ProductViewModel()
            {
                Categories = GetCategories(),
                Farms = GetFarms()
            };

            return model;
        }

        public void CreateNewProduct(ProductBindingModel model)
        {
            var product = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Quantity = model.Quantity,
                ImageUrl = model.ImageUrl,
                Units = 1,
                CategoryId = model.CategoryId,
                OwnerId = model.FarmId
            };

            this.Data.Products.Add(product);
            this.Data.SaveChanges();
        }

        public ProductViewModel GetEditProduct(int id)
        {
            Product product = this.Data.Products.Find(id);

            ProductViewModel viewModel = new ProductViewModel()
            {
                Id = product.Id,
                Categories = GetCategoriesById(product.CategoryId),
                Farms = GetFarmsById(product.OwnerId),
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };

            return viewModel;
        }

        public void EditProduct(ProductBindingModel model)
        {
            var product = Data.Products.Find(model.Id);

            product.Name = model.Name;
            product.Description = model.Description;
            product.ImageUrl = model.ImageUrl;
            product.Price = model.Price;
            product.Quantity = model.Quantity;
            product.CategoryId = model.CategoryId;
            product.OwnerId = model.FarmId;

            this.Data.SaveChanges();
        }

        public ProductViewModel GetDeleteProduct(Product product)
        {
            ProductViewModel viewModel = new ProductViewModel()
            {
                Id = product.Id,
                Categories = GetCategoriesById(product.CategoryId),
                Farms = GetFarmsById(product.OwnerId),
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };

            return viewModel;
        }

        public void DeleteProduct(int id)
        {
            Product product = this.Data.Products.Find(id);
            this.Data.Products.Remove(product);
            this.Data.SaveChanges();
        }

        private IEnumerable<SelectListItem> GetCategories()
        {
            var categories = this.Data
                        .Categories
                        .All()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name
                                });

            return new SelectList(categories, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetCategoriesById(int id)
        {
            //var productName = Data.Products.Find(id).Name;

            var categories = this.Data
                        .Categories
                        .All()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name,
                                    Selected = x.Id == id
                                });

            return new SelectList(categories, "Value", "Text", id);
        }

        private IEnumerable<SelectListItem> GetFarms()
        {
            var farms = this.Data
                        .Farms
                        .All()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name
                                });

            return new SelectList(farms, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetFarmsById(int id)
        {
            var farms = this.Data
                        .Farms
                        .All()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name,
                                    Selected = x.Id == id
                                });

            return new SelectList(farms, "Value", "Text", id);
        }
    }
}