namespace WomenMarket.Models.EntityModels
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    public class User : IdentityUser
    {
        public User()
        {
            this.ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
