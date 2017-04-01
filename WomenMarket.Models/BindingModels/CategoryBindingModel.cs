namespace WomenMarket.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    public class CategoryBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
