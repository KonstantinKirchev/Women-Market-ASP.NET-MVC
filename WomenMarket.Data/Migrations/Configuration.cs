namespace WomenMarket.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<WomenMarketContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            ContextKey = "WomenMarket.Data.WomenMarketContext";
        }

        protected override void Seed(WomenMarketContext context)
        {
            
        }
    }
}
