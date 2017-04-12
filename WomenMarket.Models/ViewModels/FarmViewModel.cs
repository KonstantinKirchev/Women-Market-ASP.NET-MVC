using System.ComponentModel.DataAnnotations;

namespace WomenMarket.Models.ViewModels
{
    public class FarmViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
    }
}