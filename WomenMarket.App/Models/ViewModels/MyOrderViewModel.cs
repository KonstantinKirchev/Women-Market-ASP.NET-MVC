using System;
using System.Collections.Generic;
using WomenMarket.Models;
using WomenMarket.Models.Enums;

namespace WomenMarket.App.Models.ViewModels
{
    public class MyOrderViewModel
    {
        public MyOrderViewModel()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime? DateOfOrder { get; set; }

        public DateTime? DateOfDelivery { get; set; }

        public OrderStatus Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}