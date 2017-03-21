using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WomenMarket.Models;

namespace WomenMarket.App.Areas.Admin.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Quantity { get; set; }

        public string ImageUrl { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Farm")]
        public int FarmId { get; set; }

        public IEnumerable<SelectListItem> Farms { get; set; }

        
    }
}