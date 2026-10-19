namespace Shopping.Web.Pages
{
    public class CheckoutModel(IBasketService basketService, ILogger<CheckoutModel> logger) : PageModel
    {
        [BindProperty]
        public BasketCheckoutModel Order { get; set; } = default!;
        public ShoppingCartModel Cart { get; set; } = default!;
        
        public async Task<IActionResult> OnGetAsync()
        {
            this.Cart = await basketService.LoadUserBasket();

            return Page();
        }

        public async Task<IActionResult> OnPostCheckoutAsync(BasketCheckoutModel order)
        {
            logger.LogInformation("CheckoutModel.OnPostCheckoutAsync called with order: {@Order}", order);

            this.Cart = await basketService.LoadUserBasket();
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            this.Order.CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");
            this.Order.UserName = this.Cart.UserName;
            this.Order.TotalPrice = this.Cart.TotalPrice;

            await basketService.CheckoutBasket(new CheckoutbasketRequest(this.Order));
            
            return RedirectToPage("Confirmation", "OrderSubmitted");            
        }
    }
}
