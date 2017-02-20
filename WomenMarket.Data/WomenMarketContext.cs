namespace WomenMarket.Data
{
    using System.Data.Entity;

    public class WomenMarketContext : DbContext
    {
        public WomenMarketContext()
            : base("WomenMarketContext")
        {
        }

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }
}