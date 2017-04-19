namespace WomenMarket.Data.Mocks
{
    using System.Data.Entity;
    using Interfaces;
    using Models.EntityModels;

    public class FakeWomenMarketContext : IWomenMarketContext
    {
        public FakeWomenMarketContext()
        {
            this.Products = new FakeProductsDbSet();
            this.Categories = new FakeCategoriesDbSet();
            this.Farms = new FakeFarmsDbSet();
            this.ShoppingCarts = new FakeShoppingCartsDbSet();
            this.ShoppingCartProducts = new FakeDbSet<ShoppingCartProduct>();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
