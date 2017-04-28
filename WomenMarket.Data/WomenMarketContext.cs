namespace WomenMarket.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using Models.EntityModels;

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

        public virtual DbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; }

        public static WomenMarketContext Create()
        {
            return new WomenMarketContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<ShoppingCart>().HasKey(q => q.Id);
            builder.Entity<Product>().HasKey(q => q.Id);
            builder.Entity<ShoppingCartProduct>().HasKey(q =>
                new {
                    q.ShoppingCartId,
                    q.ProductId
                });

            // Relationships
            builder.Entity<ShoppingCartProduct>()
                .HasRequired(t => t.ShoppingCart)
                .WithMany(t => t.ShoppingCartProducts)
                .HasForeignKey(t => t.ShoppingCartId);

            builder.Entity<ShoppingCartProduct>()
                .HasRequired(t => t.Product)
                .WithMany(t => t.ShoppingCartProducts)
                .HasForeignKey(t => t.ProductId);

            base.OnModelCreating(builder);
        }
    }
}