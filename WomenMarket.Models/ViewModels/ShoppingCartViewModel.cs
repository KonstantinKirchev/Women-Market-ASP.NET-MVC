using System.Collections.Generic;
using WomenMarket.Models.EntityModels;

namespace WomenMarket.Models.ViewModels
{
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