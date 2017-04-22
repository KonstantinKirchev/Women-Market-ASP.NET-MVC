using System.ComponentModel.DataAnnotations;

namespace WomenMarket.Models.ViewModels
{
    public class FarmViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.RequiredValidationMessage)]
        [StringLength(100, ErrorMessage = GlobalConstants.StringLengthValidationMessage)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.RequiredValidationMessage)]
        [StringLength(500, ErrorMessage = GlobalConstants.StringLengthValidationMessage)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.RequiredValidationMessage)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.RequiredValidationMessage)]
        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\b\d{3}[-.]?\d{3}[-.]?\d{4}\b", ErrorMessage = GlobalConstants.PhoneNumberValidationMessage)]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.RequiredValidationMessage)]
        [StringLength(80, ErrorMessage = GlobalConstants.StringLengthValidationMessage)]
        public string Address { get; set; }

        [Display(Name = "Image Url")]
        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.RequiredValidationMessage)]
        [Url(ErrorMessage = GlobalConstants.UrlValidationMessage)]
        public string ImageUrl { get; set; }
    }
}