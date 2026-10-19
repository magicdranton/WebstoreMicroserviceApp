using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints
{
    public record CreateOrderRequest(OrderDto Order);
    public record CreateOrderResponse(Guid Id);
    public class CreateOrderEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
            {
                var createCmd = request.Adapt<CreateOrderCommand>();
                var createOrderResult = await sender.Send(createCmd);
                var response = createOrderResult.Adapt<CreateOrderResponse>();


                return Results.Created($"/orders/{response.Id}", response);
            })
            .WithName("CreateOrder")
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Order")
            .WithDescription("Create Order");
        }
    }
}
