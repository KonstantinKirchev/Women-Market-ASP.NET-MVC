using WomenMarket.Data.UnitOfWork;
using WomenMarket.Models;
using WomenMarket.Models.EntityModels;

namespace WomenMarket.App.Services
{
    public abstract class Service
    {
        protected Service(IWomenMarketData data)
        {
            this.Data = data;
        }

        protected Service(IWomenMarketData data, User user)
            : this(data)
        {
        }

        public IWomenMarketData Data { get; private set; }
    }
}