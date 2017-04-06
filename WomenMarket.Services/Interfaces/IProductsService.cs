using System.Collections.Generic;
using WomenMarket.Models.BindingModels;
using WomenMarket.Models.ViewModels;

namespace WomenMarket.Services.Interfaces
{
    public interface IProductsService
    {
        IEnumerable<ProductViewModel> GetAllProducts();
        IEnumerable<ProductViewModel> GetFilteredProducts(string category);
        IEnumerable<ProductViewModel> GetSearchedProducts(string product);
        IEnumerable<ProductViewModel> GetProductsByFarm(string farm);
        ProductViewModel GetAddProduct();
        void CreateNewProduct(ProductBindingModel model);
        ProductViewModel GetEditProduct(int id);
        void EditProduct(ProductBindingModel model);
        ProductViewModel GetDeleteProduct(int? id);
        void DeleteProduct(int id);
    }
}