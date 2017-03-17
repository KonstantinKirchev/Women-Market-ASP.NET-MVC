﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;
using WomenMarket.Models.Enums;

namespace WomenMarket.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            this.Products = new HashSet<Product>(); 
            this.Status = OrderStatus.Pending;   
        }

        [Key]
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 99999.99, ErrorMessage = "Price should be between {1} and {2}.")]
        public decimal TotalPrice { get; set; }

        public DateTime DateOfOrder { get; set; }

        public DateTime? DateOfDelivery { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User Owner { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}