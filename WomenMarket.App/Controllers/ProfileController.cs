using WomenMarket.App.Services;
using WomenMarket.Models.BindingModels;
using WomenMarket.Models.EntityModels;
using WomenMarket.Models.ViewModels;

namespace WomenMarket.App.Controllers
{
    using System.Web.Mvc;
    using Models.ViewModels;
    using Data.UnitOfWork;
    using WomenMarket.Models;
    using System.Collections.Generic;
    using PagedList;

    public class ProfileController : BaseController
    {
        private ProfileService service;

        public ProfileController(IWomenMarketData data) 
            : base(data)
        {
            this.service = new ProfileService(data);
        }

        public ProfileController(IWomenMarketData data, User user) 
            : base(data, user)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var username = User.Identity.Name;

            UserViewModel viewModel = service.GetProfile(username);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var username = User.Identity.Name;

            UserViewModel viewModel = service.GetProfile(username);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;

                service.EditUser(username, model);
                
            }

            return RedirectToAction("Index", "Profile", routeValues: new { area = "" });
        }

        [HttpGet]
        public ActionResult MyOrders(int? page)
        {
            var username = User.Identity.Name;

            IEnumerable<MyOrderViewModel> viewModels = service.GetMyOrders(username);

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(viewModels.ToPagedList(pageNumber, pageSize));
        }

        public PartialViewResult OrdersByStatusPartial(string status, int? page)
        {
            var username = User.Identity.Name;
            

            IEnumerable<MyOrderViewModel> viewModels = service.GetOrdersByStatus(username, status);

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return PartialView(viewModels.ToPagedList(pageNumber, pageSize));
        }
    }
}