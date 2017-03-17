﻿using System.Web.Mvc;
using System.Web.UI;
using WomenMarket.App.Services;
using WomenMarket.Data.UnitOfWork;
using WomenMarket.Models;

namespace WomenMarket.App.Controllers
{
    [RoutePrefix("farms")]
    public class FarmsController : BaseController
    {
        private FarmsService service;

        public FarmsController(IWomenMarketData data) : base(data)
        {
            this.service = new FarmsService(data);
        }

        public FarmsController(IWomenMarketData data, User user) : base(data, user)
        {
        }

        [HttpGet]
        [Route]
        [OutputCache(Duration = 60, Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult All()
        {
            var farms = this.service.GetAllFarms();
            return View(farms);
        }
    }
}