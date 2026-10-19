using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints
{
    public record GetOrdersRequest(PaginationRequest request);
    public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

    public class GetOrdersEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var ordersResult = await sender.Send(new GetOrdersQuery(request));
                var ordersResponse = ordersResult.Adapt<GetOrdersResponse>();
                return Results.Ok(ordersResponse);
            })
            .WithName("GetOrders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders")
            .WithDescription("Get Orders");
        }
    }
}
