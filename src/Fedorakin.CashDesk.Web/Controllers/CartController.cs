using AutoMapper;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IStockService _stockService;
    private readonly IMapper _mapper;

    public CartController(ICartService cartService, IStockService stockService, IMapper mapper)
    {
        _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
        _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> Get(int page, int pageSize)
    {
        if (page < 1)
        {
            return BadRequest("Page must be greater than 1");
        }

        if (pageSize < 1)
        {
            return BadRequest("Page size must be greater than 1");
        }

        var carts = await _cartService.GetRange(page, pageSize, CancellationToken.None);

        if (carts.Count == 0)
        {
            return NotFound();
        }

        var response = _mapper.Map<List<CartResponse>>(carts);

        return Ok(response);
    }

    [HttpPut("{number}")]
    public async Task<IActionResult> AddProductToCart(Guid number, [FromBody] int productId)
    {
        var stock = await _stockService.GetStockForProduct(productId, CancellationToken.None);

        if (stock is null)
        {
            return NotFound("Product is not exist");
        }

        if (stock.Count <= 0)
        {
            return BadRequest("Product out of stock");
        }

        _cartService.AddProductToCart(number, stock.Product);

        return Ok("Success");
    }

    [HttpGet("{number}")]
    public async Task<IActionResult> GetCartByNumber(Guid number)
    {
        var cart = await _cartService.GetCartByNumber(number, CancellationToken.None);

        if (cart is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<CartResponse>(cart);

        return Ok(response);
    }
}
