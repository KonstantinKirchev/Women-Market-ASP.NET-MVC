namespace WomenMarket.Models.ViewModels.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
