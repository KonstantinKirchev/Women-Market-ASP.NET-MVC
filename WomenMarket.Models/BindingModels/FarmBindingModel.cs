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
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}