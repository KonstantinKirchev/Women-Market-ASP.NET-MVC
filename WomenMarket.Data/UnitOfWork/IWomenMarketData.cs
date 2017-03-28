using WomenMarket.Models.EntityModels;

namespace WomenMarket.Data.UnitOfWork
{
    using Repositories;
    using Models;

    public interface IWomenMarketData
    {
        IRepository<User> Users { get; }

        IRepository<Category> Categories { get; }

        IRepository<Farm> Farms { get; }

        IRepository<Product> Products { get; }

        IRepository<ShoppingCart> ShoppingCarts { get; }

        int SaveChanges();
    }
}
