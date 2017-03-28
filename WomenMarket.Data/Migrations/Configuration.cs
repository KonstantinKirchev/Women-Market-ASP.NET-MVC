using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WomenMarket.Models;

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
            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "User" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "Konstantin"))
            {
                var store = new UserStore<User>(context);
                var manager = new UserManager<User>(store);
                var user = new User { UserName = "Konstantin", Email = "kosta1234@abv.bg" };

                manager.Create(user, "Mama2119");
                manager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
