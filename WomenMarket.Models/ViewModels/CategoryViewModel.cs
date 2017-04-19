namespace WomenMarket.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.RequiredValidationMessage)]
        [StringLength(100, ErrorMessage = GlobalConstants.StringLengthValidationMessage)]
        public string Name { get; set; }
    }
}
