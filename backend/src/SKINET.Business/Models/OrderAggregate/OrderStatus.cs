using System.Runtime.Serialization;

namespace SKINET.Business.Models.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Payment Received")]
        PaymentReceived,

        [EnumMember(Value = "Paymnent Failed")]
        PaymentFailed,

        [EnumMember(Value = "Shiped")]
        Shipped,

        [EnumMember(Value = "Completed")]
        Completed
    }
}
