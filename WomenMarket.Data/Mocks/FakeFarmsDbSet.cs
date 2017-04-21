namespace WomenMarket.Data.Mocks
{
    using System.Linq;
    using Models.EntityModels;
    public class FakeFarmsDbSet : FakeDbSet<Farm>
    {
        public override Farm Find(params object[] keyValues)
        {
            var wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(f => f.Id == wantedId);
        }
    }
}
