namespace WomenMarket.Data.Mocks
{
    using System.Linq;
    using Models.EntityModels;

    public class FakeShoppingCartsDbSet : FakeDbSet<ShoppingCart>
    {
        public override ShoppingCart Find(params object[] keyValues)
        {
            var wantedId = (int)keyValues[0];
            return this.Set.FirstOrDefault(s => s.Id == wantedId);
        }
    }
}
