namespace WomenMarket.Data.UnitOfWork
{
    using Repositories;
    using Models.EntityModels;

    public interface IWomenMarketData
    {
        IRepository<User> Users { get; }

        IRepository<Category> Categories { get; }

        IRepository<Farm> Farms { get; }

        IRepository<Product> Products { get; }

        IRepository<ShoppingCart> ShoppingCarts { get; }

        IRepository<ShoppingCartProduct> ShoppingCartProducts { get; }

        int SaveChanges();
    }
}
