using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class AggregatorService : IAggregatorService
    {
        private readonly IBasketService _basketService;
        private readonly ICatalogService _catalogService;
        private readonly IOrderService _orderService;

        public AggregatorService(IBasketService basketService, ICatalogService catalogService, IOrderService orderService)
        {
            _basketService = basketService;
            _catalogService = catalogService;
            _orderService = orderService;
        }
        public async Task<ShoppingModel> GetShoppingInfo(string username)
        {
            var basket = await _basketService.GetBasket(username);

            for (int i = 0; i < basket.Items.Count; i++)
            {
                var product = await _catalogService
                    .GetCatalog(basket.Items[i].ProductId);
                basket.Items[i] = new BasketItemExtendedModel(basket.Items[i], product);
            }

            var orders = await _orderService.GetOrdersByUserName(username);

            var s = new ShoppingModel
            {
                UserName = username,
                BasketWithProducts = basket,
                Orders = orders
            };

            return s;
        }
    }
}
