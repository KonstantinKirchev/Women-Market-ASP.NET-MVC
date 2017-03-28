using System.ComponentModel.DataAnnotations;

namespace WomenMarket.Models.BindingModels
{
    public class FarmBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}