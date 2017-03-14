using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WomenMarket.Models
{
    public class Farm
    {
        public Farm()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}