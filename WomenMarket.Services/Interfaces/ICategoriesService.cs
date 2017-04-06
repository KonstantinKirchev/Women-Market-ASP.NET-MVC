using System.Collections.Generic;
using WomenMarket.Models.BindingModels;
using WomenMarket.Models.ViewModels;

namespace WomenMarket.Services.Interfaces
{
    public interface ICategoriesService
    {
        IEnumerable<CategoryViewModel> GetAllCategories();
        void CreateNewCategory(CategoryBindingModel model);
        CategoryViewModel GetEditCategory(int? id);
        void EditCategory(CategoryBindingModel model);
        CategoryViewModel GetDeleteCategory(int? id);
        void DeleteCategory(int id);
    }
}