namespace Ordering.Domain.Models
{
    public class OrderItem: Entity<TOrderItemId>
    {
        internal OrderItem(TOrderId orderId, TProductId productId, int quantity, decimal price)
        {
            Id = TOrderItemId.Of(Guid.NewGuid());
            this.OrderId = orderId;
            this.ProductId = productId;
            this.Quantity = quantity;
            this.Price = price;
        }
        public TOrderId OrderId { get; private set; } = default!;
        public TProductId ProductId { get; private set; } = default!;
        public int Quantity { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
    }
}
