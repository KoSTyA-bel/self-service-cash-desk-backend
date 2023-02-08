using AutoMapper;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Services;
using Fedorakin.CashDesk.Web.Contracts.Requests.SelfCheckout;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Fedorakin.CashDesk.Web.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SelfCheckoutController : ControllerBase
{
    private readonly ISelfCheckoutManager _selfCheckoutManager;
    private readonly ICartManager _cartManager;
    private readonly ICardManager _cardManager;
    private readonly ICheckManager _checkManager;
    private readonly IStockManager _stockManager;
    private readonly IDataStateManager _dataStateManager;
    private readonly ICacheService _cache;
    private readonly ISelfCheckoutService _selfCheckoutService;
    private readonly ICheckService _checkService;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;

    public SelfCheckoutController(
        ISelfCheckoutManager selfCheckoutManager, 
        ICartManager cartManager, 
        ICardManager cardManager, 
        ICheckManager checkManager, 
        IStockManager stockManager, 
        IDataStateManager dataStateManager, 
        ICacheService cache, 
        ISelfCheckoutService selfCheckoutService,
        ICheckService checkService,
        ICartService cartService,
        IMapper mapper)
    {
        _selfCheckoutManager = selfCheckoutManager ?? throw new ArgumentNullException(nameof(selfCheckoutManager));
        _cartManager = cartManager ?? throw new ArgumentNullException(nameof(cartManager));
        _cardManager = cardManager ?? throw new ArgumentNullException(nameof(cardManager));
        _checkManager = checkManager ?? throw new ArgumentNullException(nameof(checkManager));
        _stockManager = stockManager ?? throw new ArgumentNullException(nameof(stockManager));
        _dataStateManager = dataStateManager ?? throw new ArgumentNullException(nameof(dataStateManager));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _selfCheckoutService = selfCheckoutService ?? throw new ArgumentNullException(nameof(selfCheckoutService));
        _checkService = checkService ?? throw new ArgumentNullException(nameof(checkService));
        _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> Get(int page, int pageSize)
    {
        if (page < 1)
        {
            throw new InvalidPageNumberException();
        }

        if (pageSize < 1)
        {
            throw new InvalidPageSizeException();
        }

        var selfCheckouts = await _selfCheckoutManager.GetRangeAsync(page, pageSize);

        if (selfCheckouts.Count == 0)
        {
            throw new ElementNotfFoundException();
        }

        _selfCheckoutService.InsertSelfCheckoutsFromCache(selfCheckouts);

        var response = _mapper.Map<List<SelfCheckoutResponse>>(selfCheckouts);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        if (!_cache.TryGetSelfCheckout(id, out var selfCheckout))
        {
            selfCheckout = await _selfCheckoutManager.GetByIdAsync(id);
        }       

        if (selfCheckout is null)
        {
            throw new ElementNotfFoundException();
        }

        var response = _mapper.Map<SelfCheckoutResponse>(selfCheckout);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateSelfCheckoutRequest request)
    {
        var selfCheckout = _mapper.Map<SelfCheckout>(request);

        await _selfCheckoutManager.AddAsync(selfCheckout);

        await _dataStateManager.CommitChangesAsync();

        return Ok(selfCheckout.Id);
    }

    [HttpPost("Pay")]
    public async Task<IActionResult> Pay([FromBody] PayRequest request)
    {
        _ = _cache.TryGetSelfCheckout(request.SelfCheckoutId, out var selfCheckout);

        if (selfCheckout is null)
        {
            throw new ElementNotfFoundException("Can`t find self checkout");
        }

        if (selfCheckout.ActiveNumber == Guid.Empty || !selfCheckout.IsBusy)
        {
            throw new SelfCheckoutFreeException();
        }

        if (selfCheckout.ActiveNumber != request.CartNumber)
        {
            return BadRequest("Cart does not belong to self checkout");
        }

        _ = _cache.TryGetCart(request.CartNumber, out var cart);

        if (cart is null)
        {
            throw new ElementNotfFoundException("Can`t find cart");
        }

        if (cart.Products.Count == 0)
        {
            throw new CartEmptyException();
        }

        var check = new Check
        {
            SelfCheckoutId = selfCheckout.Id,
            CartNumber = request.CartNumber,
            Amount = cart.Price,
            Total = cart.Price
        };

        if (!string.IsNullOrEmpty(request.CardCode))
        {
            var card = await _cardManager.GetByCodeAsync(request.CardCode);

            if (card is not null) 
            {
                check.Card = card;
                check.Total = check.Total * (100 - card?.Discount ?? 0) / 100;
                check.Discount = check.Amount - check.Total;
                card.Total += check.Total;
                await _cardManager.UpdateAsync(card);
            }
        }

        var products = new List<Product>();

        foreach (var product in cart.Products)
        {
            var stock = await _stockManager.GetStockForProductAsync(product.Id);

            if (stock is null)
            {
                continue;
            }

            if (stock.Count > 0)
            {
                products.Add(stock.Product);

                stock.Count -= 1;

                await _stockManager.UpdateAsync(stock);
            }
        }

        cart.Products = products;

        _cartService.SetDateTime(cart);
        _checkService.SetDateTime(check);

        await _cartManager.AddAsync(cart);
        await _checkManager.AddAsync(check);

        await _dataStateManager.CommitChangesAsync();

        _cache.RemoveSelfCheckout(selfCheckout.Id);
        _cache.RemoveCart(cart.Number);

        return Ok(check.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateSelfCheckoutRequest request)
    {
        var selfCheckout = await _selfCheckoutManager.GetByIdAsync(id);

        if (selfCheckout is null)
        {
            throw new ElementNotfFoundException();
        }

        selfCheckout = _mapper.Map<SelfCheckout>(request);
        selfCheckout.Id = id;

        await _selfCheckoutManager.UpdateAsync(selfCheckout);

        await _dataStateManager.CommitChangesAsync();

        return Ok(selfCheckout.Id);
    }

    [HttpPut("Take/{id}")]
    public async Task<IActionResult> Take(int id)
    {
        _ = _cache.TryGetSelfCheckout(id, out var selfCheckout);

        if (selfCheckout is not null)
        {
            throw new SelfCheckoutBusyException();
        }

        selfCheckout = await _selfCheckoutManager.GetByIdAsync(id);

        if (selfCheckout is null)
        {
            throw new ElementNotfFoundException();
        }

        if (!selfCheckout.IsActive)
        {
            throw new SelfCheckoutUnactiveException();
        }

        _selfCheckoutService.TakeSelfCheckout(selfCheckout);

        var cart = new Cart()
        {
            Number = selfCheckout.ActiveNumber
        };

        _cache.SetCart(cart);
        _cache.SetSelfCheckout(selfCheckout);

        return Ok(cart.Number);
    }

    [HttpPost("Free")]
    public async Task<IActionResult> MakeSelfCheckoutFree(MakeSelfCheckoutFreeRequest request)
    {
        if (!_cache.TryGetSelfCheckout(request.Id, out var selfCheckout))
        {
            throw new ElementNotfFoundException();
        }

        if (selfCheckout.ActiveNumber != request.CartNumber)
        {
            throw new ElementNotfFoundException();
        }
        
        _cache.RemoveSelfCheckout(request.Id);
        _cache.RemoveCart(request.CartNumber);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var selfCheckout = await _selfCheckoutManager.GetByIdAsync(id);

        if (selfCheckout is not null)
        {
            await _selfCheckoutManager.DeleteAsync(selfCheckout);

            await _dataStateManager.CommitChangesAsync();
        }

        return Ok();
    }
}
