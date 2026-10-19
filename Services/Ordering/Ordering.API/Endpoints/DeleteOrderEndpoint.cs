using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints
{
    //public record DeleteOrderRequest(Guid Id)
    public record DeleteOrderResponse(bool IsSuccess);
    public class DeleteOrderEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{Id}", async (Guid Id, ISender sender) =>
            {
                var deleteCmd = new DeleteOrderCommand(Id);
                var deleteResult = await sender.Send(deleteCmd);
                var deleteResponse = deleteResult.Adapt<DeleteOrderResponse>();

                return Results.Ok(deleteResponse);
            })
            .WithName("DeleteOrder")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Order")
            .WithDescription("Delete Order");
        }
    }
}
