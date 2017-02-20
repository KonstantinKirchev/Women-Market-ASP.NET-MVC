using System.Data.Entity;

namespace WomenMarket.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class WomenMarketContext : IdentityDbContext<User>
    {
        public WomenMarketContext()
            : base("WomenMarketContext", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Farm> Farms { get; set; }

        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public static WomenMarketContext Create()
        {
            return new WomenMarketContext();
        }
    }
}