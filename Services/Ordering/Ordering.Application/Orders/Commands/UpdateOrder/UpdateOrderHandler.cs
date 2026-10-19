namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            // update existing order from cmd
            var orderId = TOrderId.Of(command.Order.Id);
            var order = await dbContext.Orders
                .FindAsync([orderId], cancellationToken: cancellationToken);

            if (order is null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }

            // save to DB
            UpdateOrderWithNewValues(order, command.Order);
            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            // return result
            return new UpdateOrderResult(true);
        }

        private void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
        {
            var updatedShippingAddress = TAddress.Of(
                orderDto.ShippingAddress.FirstName,
                orderDto.ShippingAddress.LastName,
                orderDto.ShippingAddress.EmailAddress,
                orderDto.ShippingAddress.AddressLine,
                orderDto.ShippingAddress.Country,
                orderDto.ShippingAddress.State,
                orderDto.ShippingAddress.ZipCode
                );

            var updatedBillingAddress = TAddress.Of(
                orderDto.BillingAddress.FirstName,
                orderDto.BillingAddress.LastName,
                orderDto.BillingAddress.EmailAddress,
                orderDto.BillingAddress.AddressLine,
                orderDto.BillingAddress.Country,
                orderDto.BillingAddress.State,
                orderDto.BillingAddress.ZipCode
                );

            var updatedPayment = TPayment.Of(
                orderDto.Payment.CardName, 
                orderDto.Payment.CardNumber, 
                orderDto.Payment.Expiration, 
                orderDto.Payment.Cvv, 
                orderDto.Payment.PaymentMethod);

            order.Update(
                name: TOrderName.Of(orderDto.OrderName),
                shippingAddress: updatedShippingAddress,
                billingAddress: updatedBillingAddress,
                payment: updatedPayment,
                status: orderDto.Status
                );
        }
    }
}
