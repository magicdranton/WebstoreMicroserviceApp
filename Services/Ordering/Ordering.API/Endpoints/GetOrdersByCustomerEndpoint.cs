using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints
{
    //public record GetOrdersByCustomerRequest(Guid CustomerId);
    public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

    public class GetOrdersByCustomerEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
            {
                var getOrdersResult = await sender.Send(new GetOrdersByCustomerQuery(customerId));
                var getOrdersResponse = getOrdersResult.Adapt<GetOrdersByCustomerResponse>();

                return Results.Ok(getOrdersResponse);
            })
            .WithName("GetOrdersByCustomer")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders By Customer")
            .WithDescription("Get Orders By Customer");
        }
    }
}
