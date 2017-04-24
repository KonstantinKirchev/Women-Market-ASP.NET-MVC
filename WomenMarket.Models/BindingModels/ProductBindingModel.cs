using System.ComponentModel.DataAnnotations;

namespace WomenMarket.Models.BindingModels
{
    public class ProductBindingModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.RequiredValidationMessage)]
        [StringLength(100, ErrorMessage = GlobalConstants.StringLengthValidationMessage)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.RequiredValidationMessage)]
        [StringLength(500, ErrorMessage = GlobalConstants.StringLengthValidationMessage)]
        public string Description { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Invalid Target Price; Maximum Two Decimal Points.")]
        [Range(0, 9999.99, ErrorMessage = "Price should be between {1} and {2}.")]
        public decimal Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.RequiredValidationMessage)]
        [StringLength(10, ErrorMessage = GlobalConstants.StringLengthValidationMessage)]
        public string Quantity { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.RequiredValidationMessage)]
        [Url(ErrorMessage = GlobalConstants.UrlValidationMessage)]
        public string ImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        public int CategoryId { get; set; }

        public int FarmId { get; set; }
    }
}