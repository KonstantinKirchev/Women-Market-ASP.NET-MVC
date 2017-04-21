namespace WomenMarket.Data.Mocks
{
    using System.Linq;
    using Models.EntityModels;

    public class FakeCategoriesDbSet : FakeDbSet<Category>
    {
        public override Category Find(params object[] keyValues)
        {
            var wantedId = (int) keyValues[0];
            return this.Set.FirstOrDefault(c => c.Id == wantedId);
        }  
    }
}
