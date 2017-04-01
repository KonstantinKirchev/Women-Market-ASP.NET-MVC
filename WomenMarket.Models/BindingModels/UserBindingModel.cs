namespace WomenMarket.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    public class UserBindingModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}