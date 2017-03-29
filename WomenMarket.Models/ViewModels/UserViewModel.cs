using System.ComponentModel.DataAnnotations;

namespace WomenMarket.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string ImageUrl { get; set; }
    }
}