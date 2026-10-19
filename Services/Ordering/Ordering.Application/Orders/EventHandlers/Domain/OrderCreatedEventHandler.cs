using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger, 
        IPublishEndpoint pubEndpoint, IFeatureManager featureMgr) : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", domEvent.GetType().Name);

            if (await featureMgr.IsEnabledAsync("OrderFullfillment"))
            {
                var orderCreatedIntergEvent = domEvent.order.ToOrderDto();

                await pubEndpoint.Publish(orderCreatedIntergEvent, cancellationToken);
            }
        }
    }
}
