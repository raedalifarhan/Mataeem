using System.Runtime.Serialization;

namespace Mataeem.Models.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Payment Recieved")]
        PaymentRecieved,

        [EnumMember(Value = "Payment Failed")]
        PaymentFailed
    }
}
