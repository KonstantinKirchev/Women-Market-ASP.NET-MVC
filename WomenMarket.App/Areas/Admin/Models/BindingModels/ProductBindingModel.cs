namespace WomenMarket.App.Areas.Admin.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using WomenMarket.Models;

    public class ProductBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Quantity { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public int FarmId { get; set; }
    }
}