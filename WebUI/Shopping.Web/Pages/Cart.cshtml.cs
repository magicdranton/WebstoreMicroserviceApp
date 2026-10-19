namespace Shopping.Web.Pages
{
    public class CartModel(IBasketService basketService, ILogger<CartModel> logger) : PageModel
    {
        public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();

        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation("Cart page requested.");

            this.Cart = await basketService.LoadUserBasket();

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveFromCartAsync(Guid productId)
        {
            logger.LogInformation("Remove from cart button clicked");

            var basket = await basketService.LoadUserBasket();
            basket.Items.RemoveAll(i => i.ProductId == productId);
            
            await basketService.StoreBasket(new StoreBasketRequest(basket));
            
            return RedirectToPage();
        }
    }
}
