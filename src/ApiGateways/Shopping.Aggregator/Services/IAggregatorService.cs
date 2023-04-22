using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public interface IAggregatorService
    {
        Task<ShoppingModel> GetShoppingInfo(string username);
    }
}
