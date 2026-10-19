using Refit;

namespace Shopping.Web.Services
{
    public interface IBasketService
    {
        [Get("/basket-service/basket/{userName}")]
        Task<GetBasketResponse> GetBasketByUserName(string userName);

        [Post("/basket-service/basket")]
        Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

        [Delete("/basket-service/basket/{userName}")]
        Task<DeleteBasketResponse> DeleteBasket(string userName);

        [Post("/basket-service/basket/checkout")]
        Task<CheckoutBasketResponse> CheckoutBasket(CheckoutbasketRequest request);

        public async Task<ShoppingCartModel> LoadUserBasket()
        {
            var usrName = "swn";
            ShoppingCartModel basket;

            try
            {
                var getBasketResponse = await GetBasketByUserName(usrName);
                basket = getBasketResponse.Cart;
            }
            catch (ApiException apiExc) when (apiExc.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                basket = new ShoppingCartModel
                {
                    UserName = usrName,
                    Items = []
                };
            }

            return basket;
        }
    }
}
