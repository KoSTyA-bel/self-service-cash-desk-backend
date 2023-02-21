using AutoMapper;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Web.Contracts.Requests.Cart;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Fedorakin.CashDesk.Web.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartManager _cartManager;
    private readonly IStockManager _stockManager;
    private readonly ICacheService _cacheService;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;

    public CartController(
        ICartManager cartManager, 
        IStockManager stockManager,
        ICacheService cacheService, 
        ICartService cartService, 
        IMapper mapper)
    {
        _cartManager = cartManager ?? throw new ArgumentNullException(nameof(cartManager));
        _stockManager = stockManager ?? throw new ArgumentNullException(nameof(stockManager));
        _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
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

        var carts = await _cartManager.GetRangeAsync(page, pageSize);

        if (carts.Count == 0)
        {
            throw new ElementNotFoundException();
        }

        var response = _mapper.Map<List<CartResponse>>(carts);

        return Ok(response);
    }

    [HttpPut()]
    public async Task<IActionResult> AddProductToCart([FromBody] AddProductToCartRequest request)
    {
        var stock = await _stockManager.GetStockForProductAsync(request.ProductId);

        if (stock is null)
        {
            throw new ElementNotFoundException("Product is not exist");
        }

        if (stock.Count <= 0)
        {
            return BadRequest("Product out of stock");
        }

        if (!_cacheService.TryGetSelfCheckout(request.SelfChecoutId, out var selfCheckout))
        {
            throw new SelfCheckoutFreeException();
        }

        if (!_cacheService.TryGetCart(request.CartNumber, out var cart))
        {
            throw new ElementNotFoundException("Cart does not exist");
        }

        if (selfCheckout.ActiveNumber != cart.Number)
        {
            // implement new exception
            throw new Exception();
        }

        _cartService.AddProduct(cart, stock.Product);

        _cacheService.SetCart(cart);
        _cacheService.SetSelfCheckout(selfCheckout);

        return Ok("Success");
    }

    [HttpGet("{number}")]
    public async Task<IActionResult> GetCartByNumber(Guid number)
    {
        _ = _cacheService.TryGetCart(number, out var cart);

        if (cart is not null)
        {
            return Ok(_mapper.Map<CartResponse>(cart));
        }

        cart = await _cartManager.GetByNumberAsync(number);

        if (cart is null)
        {
            throw new ElementNotFoundException();
        }

        var response = _mapper.Map<CartResponse>(cart);

        return Ok(response);
    }
}
