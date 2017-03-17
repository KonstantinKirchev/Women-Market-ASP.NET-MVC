namespace WomenMarket.App.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
     
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Quantity { get; set; }

        public string ImageUrl { get; set; }
    }
}