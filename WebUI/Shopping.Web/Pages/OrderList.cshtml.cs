namespace Shopping.Web.Pages
{
    public class OrderListModel(IOrderingService orderService, ILogger<OrderListModel> logger) : PageModel
    {
        public IEnumerable<OrderModel> Orders { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var customerId  = new Guid("58c49479-ec65-4de2-86e7-033c546291aa"); // Replace with the actual customer ID

            var response = await orderService.GetOrdersByCustomer(customerId);
            this.Orders = response.Orders;

            return Page();
        }
    }
}
