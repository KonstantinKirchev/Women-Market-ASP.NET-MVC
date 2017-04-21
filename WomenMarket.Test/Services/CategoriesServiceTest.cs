namespace WomenMarket.Test.Services
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WomenMarket.Services.Interfaces;
    using System.Linq;
    using AutoMapper;
    using Moq;
    using Data.UnitOfWork;
    using Models.ViewModels;
    using WomenMarket.Services;
    using Models.EntityModels;
    using Models.BindingModels;

    [TestClass]
    public class CategoriesServiceTest
    {
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
        }

        [TestMethod]
        public void GetAllCategories_ShouldReturnGivenCategories()
        {
            var categories = this._service.GetAllCategories();
            Assert.IsNotNull(categories);
            Assert.AreEqual(3, categories.Count());
            Assert.AreEqual("Fruits", categories.FirstOrDefault().Name);
        }

        [TestMethod]
        public void GetEditCategoryWithValidId_ShouldReturnGivenCategory()
        {
            var category = this._service.GetEditCategory(1);
            Assert.IsNotNull(category);
            Assert.AreEqual(1, category.Id);
            Assert.AreEqual("Fruits", category.Name);
        }

        [TestMethod]
        public void GetEditCategoryWithInvalidId_ShouldReturnNull()
        {
            var category = this._service.GetEditCategory(4);
            Assert.IsNull(category);
        }

        [TestMethod]
        public void GetDeleteCategoryWithValidId_ShouldReturnGivenCategory()
        {
            var category = this._service.GetDeleteCategory(2);
            Assert.IsNotNull(category);
            Assert.AreEqual(2, category.Id);
            Assert.AreEqual("Vegetables", category.Name);
        }

        [TestMethod]
        public void GetDeleteCategoryWithInvalidId_ShouldReturnNull()
        {
            var category = this._service.GetDeleteCategory(4);
            Assert.IsNull(category);
        }

        [TestMethod]
        public void PostValidCategory_ShouldAddToRepo()
        {
            CategoryBindingModel testCategory = new CategoryBindingModel() { Name = "Meats" };
            this._service.CreateNewCategory(testCategory);
            Assert.AreEqual(4, this.dbMock.Object.Categories.All().Count());
        }

        [TestMethod]
        public void PutValidCategory_ShouldModifyExistingCategory()
        {
            CategoryBindingModel testCategory = new CategoryBindingModel() { Id = 1, Name = "Fish" };
            this._service.EditCategory(testCategory);
            Assert.AreEqual("Fish", this.dbMock.Object.Categories.Find(1).Name);
        }

        [TestMethod]
        public void DeleteExistingCategory_ShouldDeleteCategoryFromRepo()
        {
            this._service.DeleteCategory(2);
            Assert.AreEqual(2, this.dbMock.Object.Categories.All().Count());
        }

        [TestMethod]
        public void DeleteNotExistingCategory_ShouldNotDeleteCategoryFromRepo()
        {
            this._service.DeleteCategory(4);
            Assert.AreEqual(3, this.dbMock.Object.Categories.All().Count());
        }
    }
}
