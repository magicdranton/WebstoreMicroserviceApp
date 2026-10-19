using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints
{
    //public record GetOrdersByNameRequest(string Name);
    public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByNameEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
            {
                var ordersResult = await sender.Send(new GetOrdersByNameQuery(orderName));
                var response = ordersResult.Adapt<GetOrdersByNameResponse>();

                return Results.Ok(response);
            })
            .WithName("GetOrdersByName")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders By Name")
            .WithDescription("Get Orders By Name");
        }
    }
}
