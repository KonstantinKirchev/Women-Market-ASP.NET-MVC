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

        public static WomenMarketContext Create()
        {
            return new WomenMarketContext();
        }
    }
}