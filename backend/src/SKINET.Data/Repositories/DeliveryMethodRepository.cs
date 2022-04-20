using SKINET.Business.Interfaces;
using SKINET.Business.Models.OrderAggregate;
using SKINET.Data.Context;

namespace SKINET.Data.Repositories
{
    public class DeliveryMethodRepository : Repository<DeliveryMethod>, IDeliveryMethodRepository
    {
        public DeliveryMethodRepository(StoreContext dbContext) : base(dbContext)
        {
        }
    }
}
