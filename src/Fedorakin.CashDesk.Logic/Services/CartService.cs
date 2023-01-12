using Fedorakin.CashDesk.Logic.Interfaces;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Fedorakin.CashDesk.Logic.Services;

public class CartService : ServiceBase<Cart>, ICartService
{
    private readonly IMemoryCache _cache;

    public CartService(ICartProvider provider, ICartRepository repository, IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        : base(provider, repository, unitOfWork)
    {
        _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public void AddProductsToCart(Guid cartNumber, List<Product> products)
    {
        if (products is null)
        {
            throw new ArgumentNullException(nameof(products));
        }

        if (products.Count == 0)
        {
            throw new ArgumentException("List can`t be empry");
        }

        if (products.Any(x => x is null))
        {
            throw new ArgumentNullException(nameof(products), "Product can`t be null");
        }

        var cart = GetCartInCache(cartNumber);

        cart.Products.AddRange(products);

        foreach (var product in products)
        {
            cart.Price += product.Price;
        }

        _cache.Set(cartNumber, cart, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
    }

    public void AddProductToCart(Guid cartNumber, Product product)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        var cart = GetCartInCache(cartNumber);

        cart.Products.Add(product);
        cart.Price += product.Price;

        _cache.Set(cartNumber, cart, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
    }

    public Task<Cart?> GetByCartNumber(Guid cartNumber, CancellationToken cancellationToken)
    {
        var obj = _cache.Get(cartNumber);

        return Task.FromResult(obj as Cart);
    }

    public Task<Cart?> TakeCart(Guid cartNumber, CancellationToken cancellationToken)
    {
        var cart = new Cart { Number = cartNumber };

        _cache.Set(cartNumber, cart, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));

        return Task.FromResult(cart);
    }

    private Cart GetCartInCache(Guid cartNumber)
    {
        if (_cache.TryGetValue(cartNumber, out object obj))
        {
            return obj as Cart;
        }

        throw new Exception();
    }
}
