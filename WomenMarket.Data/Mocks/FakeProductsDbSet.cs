namespace WomenMarket.Data.Mocks
{
    using System.Linq;
    using Models.EntityModels;

    public class FakeProductsDbSet : FakeDbSet<Product>
    {
        public override Product Find(params object[] keyValues)
        {
            var wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(p => p.Id == wantedId);
        }
    }
}
