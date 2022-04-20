using SKINET.Business.Interfaces;
using SKINET.Business.Models.OrderAggregate;
using SKINET.Data.Context;

namespace SKINET.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(StoreContext dbContext) : base(dbContext)
        {
        }
    }
}
