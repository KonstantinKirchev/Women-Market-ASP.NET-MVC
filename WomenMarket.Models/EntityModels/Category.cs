﻿using System.Collections.Generic;
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

        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.RequiredValidationMessage)]
        [StringLength(100, ErrorMessage = GlobalConstants.StringLengthValidationMessage)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}