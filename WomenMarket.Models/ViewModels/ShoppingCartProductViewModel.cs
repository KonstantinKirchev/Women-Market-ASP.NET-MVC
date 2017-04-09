using WomenMarket.Models.EntityModels;

namespace WomenMarket.Models.ViewModels
{
    public class ShoppingCartProductViewModel
    {
        public int ShoppingCartId { get; set; }

        public int ProductId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        public virtual Product Product { get; set; }

        public int Units { get; set; }
    }
}
