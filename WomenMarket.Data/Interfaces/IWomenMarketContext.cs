namespace WomenMarket.Data.Interfaces
{
    using System.Data.Entity;
    using Models.EntityModels;

    public interface IWomenMarketContext
    {

        DbSet<Product> Products { get; set; }

        DbSet<Category> Categories { get; set; }

        DbSet<Farm> Farms { get; set; }

        DbSet<ShoppingCart> ShoppingCarts { get; set; }

        DbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; }

        int SaveChanges();
    }
}
