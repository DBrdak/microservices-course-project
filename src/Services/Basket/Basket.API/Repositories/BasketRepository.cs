using AutoMapper;
using Basket.API.Entities;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _cache;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public BasketRepository(IDistributedCache cache, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _cache = cache;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<ShoppingCart> GetBasket(string userName)
    {
        var basket = await _cache.GetStringAsync(userName);

        if (string.IsNullOrEmpty(basket))
            return new ShoppingCart();

        return JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
    {
        await _cache.SetStringAsync(basket.UserName,
            JsonConvert.SerializeObject(basket));

        return await GetBasket(basket.UserName);
    }

    public async Task DeleteBasket(string userName)
    {
        await _cache.RemoveAsync(userName);
    }

    public async Task<bool> Checkout(BasketCheckout basketCheckout)
    {
        var basket = await GetBasket(basketCheckout.UserName);

        if (basket is null) return false;

        var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
        eventMessage.TotalPrice = basket.TotalPrice;
        await _publishEndpoint.Publish<BasketCheckoutEvent>(eventMessage);

        await DeleteBasket(basket.UserName);

        return true;
    }
}