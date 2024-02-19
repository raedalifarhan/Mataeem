namespace Mataeem.Models
{
    public class CustomerBasket : BaseEntity
    {
        public CustomerBasket()
        {
        }

        public CustomerBasket(Guid id)
        {
            this.Id = id;
        }

        public List<BasketItem>? Items { get; set; } = new();

        public int? DeliveryMethod { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntendId { get; set; }

        public AppUser Customer { get; set; }
        public string CustomerId { get; set; }
    }
}
