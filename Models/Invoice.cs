using Mataeem.Models.OrderAggregate;
using System.ComponentModel.DataAnnotations;

namespace Mataeem.Models
{
    public class Invoice : BaseEntity
    {
        [Required]
        public string InvoiceNumber { get; set; } 

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        public Order? Order { get; set; }
        public Guid? OrderId { get; set; }

        public AppUser? Customer { get; set; }
        public string? CustomerId { get; set; }

        public AppUser? Driver { get; set; }
        public string? DriverId { get; set; }

        public DateTime? DeliveryDate { get; set; }

        // رسوم الشحن
        public decimal? ShippingFee { get; set; }

        // ضرائب الفاتورة
        public decimal? Taxes { get; set; }

        // معلومات الدفع
        public string? PaymentMethod { get; set; }

        public DateTime? ExpectedDeliveryTime { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public AppUser? CreatedBy { get; set; }
        public string? CreatedById { get; set; }

        public AppUser? UpdatedBy { get; set; }
        public string? UpdatedById { get; set; }
    }
}
