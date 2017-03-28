using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WomenMarket.Models.EntityModels
{
    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}