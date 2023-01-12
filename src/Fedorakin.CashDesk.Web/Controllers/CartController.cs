using AutoMapper;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
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

    [HttpPut("{cartNumber}")]
    public async Task<IActionResult> AddProductToCart(Guid cartNumber, [FromBody] int productId)
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

        _cartService.AddProductToCart(cartNumber, stock.Product);

        return Ok("Succes");
    }
}
