namespace WomenMarket.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.EntityModels;

    [TestClass]
    public class FarmsControllerTest
    {
        private MockContainer mocks;

        [TestInitialize]
        public void InitTest()
        {
            this.mocks = new MockContainer();
            this.mocks.PrepareMocks();
        }

        [TestMethod]
        public void GetAll_WhenValid_ShouldReturnFarmsCollection()
        {
            //Arrange and Act
            var fakeFarms = this.mocks.FarmRepositoryMock.Object.All();
            var firstFarm = this.mocks.FarmRepositoryMock.Object.Find(1);

            // Assert 
            Assert.AreEqual(3, fakeFarms.Count());
            Assert.AreEqual("Dido", firstFarm.Name);
        }

        [TestMethod]
        public void AddFarmShouldIncreaseFarmCountByOne()
        {
            var farms = new List<Farm>();

            Farm farm = new Farm()
            {
                Name = "Dido",
                Address = "Dianabad 43-44",
                Description = "Very cheap food",
                Email = "dido@dido.com",
                ImageUrl = "www.google.com",
                PhoneNumber = "4165580102"
            };

            var farmsCount = farms.Count;

            farms.Add(farm);

            var newFarmsCount = farms.Count;

            Assert.AreEqual(0, farmsCount);
            Assert.AreEqual(1, newFarmsCount);
        }
    }
}
