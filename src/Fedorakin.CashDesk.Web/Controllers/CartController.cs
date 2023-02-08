using AutoMapper;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
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
    private readonly IDataStateManager _dataStateManager;
    private readonly ICacheService _cacheService;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;

    public CartController(
        ICartManager cartManager, 
        IStockManager stockManager, 
        IDataStateManager dataStateManager, 
        ICacheService cacheService, 
        ICartService cartService, 
        IMapper mapper)
    {
        _cartManager = cartManager ?? throw new ArgumentNullException(nameof(cartManager));
        _stockManager = stockManager ?? throw new ArgumentNullException(nameof(stockManager));
        _dataStateManager = dataStateManager ?? throw new ArgumentNullException(nameof(dataStateManager));
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
            throw new ElementNotfFoundException();
        }

        var response = _mapper.Map<List<CartResponse>>(carts);

        return Ok(response);
    }

    [HttpPut("{number}")]
    public async Task<IActionResult> AddProductToCart(Guid number, [FromBody] int productId)
    {
        var stock = await _stockManager.GetStockForProductAsync(productId);

        if (stock is null)
        {
            throw new ElementNotfFoundException("Product is not exist");
        }

        if (stock.Count <= 0)
        {
            return BadRequest("Product out of stock");
        }

        if (!_cacheService.TryGetCart(number, out var cart))
        {
            throw new ElementNotfFoundException("Cart does not exist");
        }

        _cartService.AddProduct(cart, stock.Product);

        _cacheService.SetCart(cart);

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
            throw new ElementNotfFoundException();
        }

        var response = _mapper.Map<CartResponse>(cart);

        return Ok(response);
    }
}
