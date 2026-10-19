using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginatedRequest.PageIndex;
            var pageSize = query.PaginatedRequest.PageSize;
            var totalCnt = await dbContext.Orders.LongCountAsync(cancellationToken);

            // get orders with pagination
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .OrderBy(o => o.OrderName.Value)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            // return result
            return new GetOrdersResult(
                new PaginatedResult<OrderDto>(
                    pageIndex,
                    pageSize,
                    totalCnt,
                    orders.ToOrderDtoList()
                    )
                );
        }
    }
}
