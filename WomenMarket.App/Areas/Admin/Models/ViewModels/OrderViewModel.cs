using System;
using System.Collections.Generic;
using WomenMarket.Models;
using WomenMarket.Models.Enums;

namespace WomenMarket.App.Areas.Admin.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime? DateOfOrder { get; set; }

        public DateTime? DateOfDelivery { get; set; }

        public User Owner { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}