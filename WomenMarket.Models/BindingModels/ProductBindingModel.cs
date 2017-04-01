using System.ComponentModel.DataAnnotations;

namespace WomenMarket.Models.BindingModels
{
    public class ProductBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string Quantity { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public int FarmId { get; set; }
    }
}