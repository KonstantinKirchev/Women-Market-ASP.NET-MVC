namespace WomenMarket.Test.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using App.Areas.Admin.Controllers;
    using WomenMarket.Services.Interfaces;
    using Data.UnitOfWork;
    using WomenMarket.Services;

    [TestClass]
    public class CategoriesControllerTest
    {
        private CategoriesController _controller;
        private ICategoriesService _service;
        private IWomenMarketData _data;

        [TestInitialize]
        public void Init()
        {
            this._service = new CategoriesService(this._data);
            this._controller = new CategoriesController(this._data, this._service);
        }

        [TestMethod]
        public void Index_ShouldPass()
        {

        }
    }
}
