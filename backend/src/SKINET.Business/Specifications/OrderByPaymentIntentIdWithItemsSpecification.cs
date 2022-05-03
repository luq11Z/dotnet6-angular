using SKINET.Business.Models.OrderAggregate;
using SKINET.Data.Specifications;

namespace SKINET.Business.Specifications
{
    public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
    {
        public OrderByPaymentIntentIdSpecification(string paymentIntentId) 
            : base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
