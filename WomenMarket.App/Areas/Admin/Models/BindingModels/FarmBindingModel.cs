﻿using System.ComponentModel.DataAnnotations;

namespace WomenMarket.App.Areas.Admin.Models.BindingModels
{
    public class FarmBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}