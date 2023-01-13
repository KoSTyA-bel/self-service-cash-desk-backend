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
    private readonly IStockService _stockService;
    private new ICartProvider _cartProvider;
    private ITimeSpanProvider _timeSpanProvider;
    private IDateTimeProvider _dateTimeProvider;

    public CartService(ICartProvider provider,
        ICartRepository repository,
        IUnitOfWork unitOfWork,
        IStockService stockService,
        ITimeSpanProvider timeSpanProvider,
        IDateTimeProvider dateTimeProvider,
        IMemoryCache memoryCache)
        : base(provider, repository, unitOfWork)
    {
        _cartProvider = provider ?? throw new ArgumentNullException(nameof(provider));
        _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _stockService = stockService ?? throw new ArgumentNullException(nameof(memoryCache));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        _timeSpanProvider = timeSpanProvider ?? throw new ArgumentNullException(nameof(timeSpanProvider));
    }

    public override Task Create(Cart entity, CancellationToken cancellationToken)
    {
        return base.Create(entity, cancellationToken);     
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

        _cache.Set(cartNumber, cart, new MemoryCacheEntryOptions().SetAbsoluteExpiration(_timeSpanProvider.ForFiveMinutes()));
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

        _cache.Set(cartNumber, cart, new MemoryCacheEntryOptions().SetAbsoluteExpiration(_timeSpanProvider.ForFiveMinutes()));
    }

    public Task<Cart?> GetCachedCartByNumber(Guid cartNumber, CancellationToken cancellationToken)
    {
        var obj = _cache.Get(cartNumber);

        return Task.FromResult(obj as Cart);
    }

    public Task<Cart?> GetCartByNumber(Guid number, CancellationToken cancellationToken)
    {
        return _cartProvider.GetCartByNumber(number, cancellationToken);
    }

    public Task<Cart?> TakeCart(Guid cartNumber, CancellationToken cancellationToken)
    {
        var cart = new Cart { Number = cartNumber };

        _cache.Set(cartNumber, cart, new MemoryCacheEntryOptions().SetAbsoluteExpiration(_timeSpanProvider.ForFiveMinutes()));

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
