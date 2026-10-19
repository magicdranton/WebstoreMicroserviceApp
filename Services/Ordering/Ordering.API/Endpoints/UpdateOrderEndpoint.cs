using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints
{
    public record UpdateOrderRequest(OrderDto Order);
    public record UpdateOrderResponse(bool IsSuccess);
    public class UpdateOrderEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) => 
            {
                var updateCmd = request.Adapt<UpdateOrderCommand>();
                var updateResult = await sender.Send(updateCmd);
                var updateResponse = updateResult.Adapt<UpdateOrderResponse>();

                return Results.Ok(updateResponse);
            })
            .WithName("UpdateOrder")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Order")
            .WithDescription("Update Order");
        }
    }
}
