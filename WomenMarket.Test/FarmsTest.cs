using System.Collections.Generic;
using WomenMarket.Data;
using WomenMarket.Models.EntityModels;

namespace WomenMarket.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FarmsTest
    {
        //public FarmsTest(IWomenMarketData data)
        //{
        //    this.Data = data;
        //}

        //public IWomenMarketData Data { get; }

        WomenMarketContext context = new WomenMarketContext();

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
