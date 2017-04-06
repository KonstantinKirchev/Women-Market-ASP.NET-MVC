using WomenMarket.Services.Interfaces;

namespace WomenMarket.Services
{
    using Data.UnitOfWork;
    using Models.EntityModels;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Models.BindingModels;
    using Models.ViewModels;

    public class CategoriesService : Service, ICategoriesService
    {
        public CategoriesService(IWomenMarketData data) 
            : base(data)
        {
        }

        public CategoriesService(IWomenMarketData data, User user) 
            : base(data, user)
        {
        }

        public IEnumerable<CategoryViewModel> GetAllCategories()
        {
            IEnumerable<Category> categories = this.Data.Categories.All().OrderBy(c => c.Id);
            IEnumerable<CategoryViewModel> viewModels = Mapper.Instance.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);

            return viewModels;
        }

        public void CreateNewCategory(CategoryBindingModel model)
        {
            var category = new Category()
            {
                Name = model.Name
            };

            this.Data.Categories.Add(category);
            this.Data.SaveChanges();
        }

        public CategoryViewModel GetEditCategory(int? id)
        {
            Category category = this.Data.Categories.Find(id);

            if (category == null)
            {
                return null;
            }

            CategoryViewModel viewModel = Mapper.Instance.Map<Category, CategoryViewModel>(category);

            return viewModel;
        }

        public void EditCategory(CategoryBindingModel model)
        {
            Category category = this.Data.Categories.Find(model.Id);

            category.Name = model.Name;

            this.Data.SaveChanges();
        }

        public CategoryViewModel GetDeleteCategory(int? id)
        {
            Category categeory = this.Data.Categories.Find(id);

            if (categeory == null)
            {
                return null;
            }

            CategoryViewModel viewModel = Mapper.Instance.Map<Category, CategoryViewModel>(categeory);

            return viewModel;
        }

        public void DeleteCategory(int id)
        {
            Category category = this.Data.Categories.Find(id);
            this.Data.Categories.Remove(category);

            this.Data.SaveChanges();
        }
    }
}
