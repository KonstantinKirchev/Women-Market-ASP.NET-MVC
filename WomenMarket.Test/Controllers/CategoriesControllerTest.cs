namespace WomenMarket.Test.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using App.Areas.Admin.Controllers;
    using WomenMarket.Services.Interfaces;
    using Data.UnitOfWork;
    using WomenMarket.Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Moq;
    using Models.BindingModels;
    using Models.EntityModels;
    using Models.ViewModels;
    using System.Net;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class CategoriesControllerTest
    {
        private CategoriesController _controller;
        private MockContainer mocks;
        private Mock<IWomenMarketData> dbMock;
        private ICategoriesService _service;

        private void ConfigureMapper()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Category, CategoryViewModel>();
                expression.CreateMap<CategoryBindingModel, Category>();
            });
        }

        [TestInitialize]
        public void Init()
        {
            this.ConfigureMapper();

            this.dbMock = new Mock<IWomenMarketData>();
            this.mocks = new MockContainer();
            this.mocks.PrepareMocks();

            this.dbMock.Setup(c => c.Categories).Returns(this.mocks.CategoryRepositoryMock.Object);

            this._service = new CategoriesService(this.dbMock.Object);
            this._controller = new CategoriesController(this.dbMock.Object, this._service);
        }

        [TestMethod]
        public void Index_ShouldPass()
        {
            var result = _controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName));

            _controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void Index_ShouldPassCorrectModelToTheView()
        {
            var result = _controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            var categories = result.Model as IEnumerable<CategoryViewModel>;
            Assert.IsNotNull(categories);
            Assert.AreEqual(3, categories.Count());

            _controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView()
                .WithModel<IEnumerable<CategoryViewModel>>();
        }

        [TestMethod]
        public void Create_ShouldPass()
        {
            _controller.WithCallTo(c => c.Create())
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void CreateNullModel_ShouldReturnDefaultView()
        {
            CategoryBindingModel model = null;
            _controller.WithCallTo(c => c.Create(model))
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void CreateValidModel_ShouldRedirectToIndex()
        {
            CategoryBindingModel model = new CategoryBindingModel()
            {
                Id = 4,
                Name = "Chushki"
            };

            _controller.WithCallTo(c => c.Create(model))
                .ShouldRedirectTo(c => c.Index);
        }

        [TestMethod]
        public void EditValidId_ShouldPass()
        {
            _controller.WithCallTo(c => c.Edit(1))
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void EditInValidId_ShouldReturnNotFound()
        {
            _controller.WithCallTo(c => c.Edit(4))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void EditNullModel_ShouldReturnDefaultView()
        {
            CategoryBindingModel model = null;
            _controller.WithCallTo(c => c.Edit(model))
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void EditValidModel_ShouldRedirectToIndex()
        {
            CategoryBindingModel model = new CategoryBindingModel()
            {
                Id = 3,
                Name = "Chushki"
            };

            _controller.WithCallTo(c => c.Edit(model))
                .ShouldRedirectTo(c => c.Index);
        }

        [TestMethod]
        public void EditNullModel_ShouldRenderDefaultView()
        {
            CategoryBindingModel model = null;

            _controller.WithCallTo(c => c.Edit(model))
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void DeleteInValidId_ShouldReturnNotFound()
        {
            _controller.WithCallTo(c => c.Delete(4))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void DeleteValidId_ShouldRenderDeleteView()
        {
            _controller.WithCallTo(c => c.Delete(2))
                .ShouldRenderView("Delete");
        }

        [TestMethod]
        public void DeleteValidId_ShouldPassCorrectModelToTheView()
        {
            _controller.WithCallTo(c => c.Delete(2)).ShouldRenderDefaultView()
                .WithModel<CategoryViewModel>();
            _controller.WithCallTo(c => c.Delete(2)).ShouldRenderDefaultView()
                .WithModel<CategoryViewModel>(m => m.Name == "Vegetables");
        }

        [TestMethod]
        public void DeleteValidId_ShouldRedirectToIndex()
        {
            _controller.WithCallTo(c => c.DeleteConfirmed(2))
                .ShouldRedirectTo(c => c.Index);
        }
    }
}
