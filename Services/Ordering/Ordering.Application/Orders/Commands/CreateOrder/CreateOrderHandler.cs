namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            // create order entity from cmd
            var order = CreateNewOrder(command.Order);

            // save to db
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            // return result
            return new CreateOrderResult(order.Id.Value);
        }

        private Order CreateNewOrder(OrderDto orderDto)
        {
            var shippingAddress = TAddress.Of(
                orderDto.ShippingAddress.FirstName,
                orderDto.ShippingAddress.LastName,
                orderDto.ShippingAddress.EmailAddress,
                orderDto.ShippingAddress.AddressLine,
                orderDto.ShippingAddress.Country,
                orderDto.ShippingAddress.State,
                orderDto.ShippingAddress.ZipCode
                );

            var billingAddress = TAddress.Of(
                orderDto.BillingAddress.FirstName,
                orderDto.BillingAddress.LastName,
                orderDto.BillingAddress.EmailAddress,
                orderDto.BillingAddress.AddressLine,
                orderDto.BillingAddress.Country,
                orderDto.BillingAddress.State,
                orderDto.BillingAddress.ZipCode
                );

            var newOrder = Order.Create(
                id: TOrderId.Of(Guid.NewGuid()),
                customerId: TCustomerId.Of(orderDto.CustomerId),
                name: TOrderName.Of(orderDto.OrderName),
                shippingAddress: shippingAddress,
                billingAddress: billingAddress,
                payment: TPayment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod)
                );

            foreach (var orderItemDto in orderDto.OrderItems)
            {
                newOrder.AddOrderItem(
                    TProductId.Of(orderItemDto.ProductId),
                    orderItemDto.Quantity,
                    orderItemDto.Price
                    );
            }

            return newOrder;
        }
    }
}
