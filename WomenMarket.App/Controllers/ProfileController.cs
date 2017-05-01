namespace WomenMarket.App.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using System.Collections.Generic;
    using PagedList;
    using WomenMarket.Models.BindingModels;
    using WomenMarket.Models.EntityModels;
    using WomenMarket.Models.ViewModels;
    using Services.Interfaces;

    public class ProfileController : BaseController
    {
        private IProfileService service;

        public ProfileController(IWomenMarketData data, IProfileService service) 
            : base(data)
        {
            this.service = service;
        }

        public ProfileController(IWomenMarketData data, User user) 
            : base(data, user)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            UserViewModel viewModel = service.GetProfile(this.UserProfile);

            TempData["controllerName"] = this.ControllerContext.RouteData.Values["controller"].ToString();

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            UserViewModel viewModel = service.GetProfile(this.UserProfile);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                service.EditUser(this.UserProfile, model);

                string controller = TempData["controllerName"].ToString();

                return RedirectToAction("Index", controller, routeValues: new { area = "" });

            }

            return this.View();
        }

        [HttpGet]
        public ActionResult MyOrders(int? page)
        {
            IEnumerable<MyOrderViewModel> viewModels = service.GetMyOrders(this.UserProfile);

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(viewModels.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public PartialViewResult _OrdersByStatusPartial(string status, int? page)
        {            
            IEnumerable<MyOrderViewModel> viewModels = service.GetOrdersByStatus(this.UserProfile, status);

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return PartialView(viewModels.ToPagedList(pageNumber, pageSize));
        }
    }
}