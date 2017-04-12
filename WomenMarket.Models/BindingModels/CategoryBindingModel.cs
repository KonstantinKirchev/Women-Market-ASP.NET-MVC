namespace WomenMarket.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    public class CategoryBindingModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.RequiredValidationMessage)]
        [StringLength(100, ErrorMessage = GlobalConstants.StringLengthValidationMessage)]
        public string Name { get; set; }
    }
}
