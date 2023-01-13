using AutoMapper;
using Azure.Core;
using Fedorakin.CashDesk.Logic.Exceptions;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;
using Fedorakin.CashDesk.Web.Contracts.Requests.SelfCheckout;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SelfCheckoutController : ControllerBase
{
    private readonly ISelfCheckoutService _selfCheckoutService;
    private readonly ICartService _cartService;
    private readonly ICardService _cardService;
    private readonly ICheckService _checkService;
    private readonly IMapper _mapper;

    public SelfCheckoutController(ISelfCheckoutService selfCheckoutService, ICartService cartService, ICardService cardService, ICheckService checkService, IMapper mapper)
    {
        _selfCheckoutService = selfCheckoutService ?? throw new ArgumentNullException(nameof(selfCheckoutService));
        _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
        _cardService = cardService ?? throw new ArgumentNullException(nameof(cardService));
        _checkService = checkService ?? throw new ArgumentNullException(nameof(checkService));
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

        var selfCheckouts = await _selfCheckoutService.GetRange(page, pageSize, CancellationToken.None);

        if (selfCheckouts.Count == 0)
        {
            return NotFound();
        }

        var response = _mapper.Map<List<SelfCheckoutResponse>>(selfCheckouts);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var selfCheckout = await _selfCheckoutService.Get(id, CancellationToken.None);

        if (selfCheckout is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<SelfCheckoutResponse>(selfCheckout);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateSelfCheckoutRequest request)
    {
        var selfCheckout = _mapper.Map<SelfCheckout>(request);

        await _selfCheckoutService.Create(selfCheckout, CancellationToken.None);

        return Ok(selfCheckout.Id);
    }

    [HttpPost("Pay")]
    public async Task<IActionResult> Pay([FromBody] PayRequest request)
    {
        var selfCheckout = await _selfCheckoutService.Get(request.SelfCheckoutId, CancellationToken.None);

        if (selfCheckout is null)
        {
            return NotFound("Can`t find self checkout");
        }

        if (selfCheckout.ActiveNumber == Guid.Empty)
        {
            return BadRequest("Self checkout is not busy");
        }

        if (selfCheckout.ActiveNumber != request.CartNumber)
        {
            return BadRequest("Cart does not belong to self checkout");
        }

        var cart = await _cartService.GetCachedCartByNumber(request.CartNumber, CancellationToken.None);

        if (cart is null)
        {
            return NotFound("Can`t find cart");
        }

        if (cart.Products.Count == 0)
        {
            return BadRequest("Cart is empty");
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
            var card = await _cardService.GetCardByCode(request.CardCode, CancellationToken.None);

            if (card is not null) 
            {
                check.Card = card;
                check.Total = check.Total * (100 - card?.Discount ?? 0) / 100;
                check.Discount = check.Amount - check.Total;
                card.Total += check.Total;
                await _cardService.Update(card, CancellationToken.None);
            }
        }

        await _cartService.Create(cart, CancellationToken.None);
        await _checkService.Create(check, CancellationToken.None);
        await _selfCheckoutService.Free(selfCheckout.Id, CancellationToken.None);

        return Ok(check.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateSelfCheckoutRequest request)
    {
        var selfCheckout = await _selfCheckoutService.Get(id, CancellationToken.None);

        if (selfCheckout is null)
        {
            return NotFound();
        }

        selfCheckout = _mapper.Map<SelfCheckout>(request);
        selfCheckout.Id = id;

        await _selfCheckoutService.Update(selfCheckout, CancellationToken.None);

        return Ok(selfCheckout.Id);
    }

    [HttpPut("Take/{id}")]
    public async Task<IActionResult> Take(int id)
    {
        try
        {
            var cartNumber = await _selfCheckoutService.TakeSelfCheckout(id, CancellationToken.None);

            await _cartService.TakeCart(cartNumber, CancellationToken.None);

            return Ok(cartNumber);
        }
        catch (SelfCheckoutBusyException)
        {
            return BadRequest("Self checkout is busy");
        }
        catch (SelfCheckoutUnactiveException)
        {
            return BadRequest("Self checkout is not active");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _selfCheckoutService.Delete(id, CancellationToken.None);

        return Ok();
    }
}
