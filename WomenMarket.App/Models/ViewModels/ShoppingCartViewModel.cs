namespace WomenMarket.App.Models.ViewModels
{
    using System.Collections.Generic;
    using WomenMarket.Models;

    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}