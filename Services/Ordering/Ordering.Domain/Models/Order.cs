namespace Ordering.Domain.Models
{
    public class Order: Aggregate<TOrderId>
    {
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public TCustomerId CustomerId { get; private set; } = default!;
        public TOrderName OrderName { get; private set; } = default!;
        public TAddress ShippingAddress { get; private set; } = default!;
        public TAddress BillingAddress { get; private set; } = default!;
        public TPayment Payment { get; private set; } = default!;
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public decimal TotalPrice
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { }
        }

        public static Order Create(TOrderId id, TCustomerId customerId, TOrderName name, TAddress shippingAddress, TAddress billingAddress, TPayment payment)
        {
            var order = new Order
            {
                Id = id,
                CustomerId = customerId,
                OrderName = name,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment,
                Status = OrderStatus.Pending
            };

            order.AddDomainEvent(new OrderCreatedEvent(order));

            return order;
        }

        public void Update(TOrderName name, TAddress shippingAddress, TAddress billingAddress, TPayment payment, OrderStatus status)
        {
            this.OrderName = name;
            this.ShippingAddress = shippingAddress;
            this.BillingAddress = billingAddress;
            this.Payment = payment;
            this.Status = status;

            this.AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void AddOrderItem(TProductId productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var orderItem = new OrderItem(Id, productId, quantity, price);
            _orderItems.Add(orderItem);
        }

        public void RemoveOrderItem(TProductId productId)
        {
            var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);

            if (orderItem is not null)
            {
                _orderItems.Remove(orderItem);
            }
        }
    }
}
