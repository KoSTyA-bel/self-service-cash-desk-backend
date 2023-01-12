using AutoMapper;
using Azure.Core;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;
using Fedorakin.CashDesk.Web.Contracts.Requests.Role;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardController : ControllerBase
{
    private readonly ICardService _service;
    private readonly IMapper _mapper;

    public CardController(ICardService service, IMapper mapper)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> Get(int? page, int? pageSize)
    {
        var cards = await _service.GetRange(page.Value, pageSize.Value, CancellationToken.None);

        if (cards.Count == 0)
        {
            return NotFound();
        }

        var response = _mapper.Map<List<CardResponse>>(cards);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var card = await _service.Get(id, CancellationToken.None);

        if (card is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<CardResponse>(card);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateRoleRequest request)
    {
        var card = _mapper.Map<Card>(request);

        await _service.Create(card, CancellationToken.None);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateRoleRequest request)
    {
        var card = _mapper.Map<Card>(request);
        card.Id = id;

        await _service.Update(card, CancellationToken.None);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id, CancellationToken.None);

        return Ok();
    }
}
