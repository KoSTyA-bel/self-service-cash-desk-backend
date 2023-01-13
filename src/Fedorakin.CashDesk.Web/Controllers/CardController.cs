using AutoMapper;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;
using Fedorakin.CashDesk.Web.Contracts.Requests.Card;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardController : ControllerBase
{
    private readonly ICardService _cardService;
    private readonly IProfileService _profileService;
    private readonly IMapper _mapper;

    public CardController(ICardService cardService, IProfileService profileService, IMapper mapper)
    {
        _cardService = cardService ?? throw new ArgumentNullException(nameof(cardService));
        _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
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

        var cards = await _cardService.GetRange(page, pageSize, CancellationToken.None);

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
        var card = await _cardService.Get(id, CancellationToken.None);

        if (card is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<CardResponse>(card);

        return Ok(response);
    }

    [HttpGet("GetByCode/{code}")]
    public async Task<IActionResult> Get(string code)
    {
        var card = await _cardService.GetCardByCode(code, CancellationToken.None);

        if (card is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<CardResponse>(card);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCardRequest request)
    {
        var profile = await _profileService.Get(request.ProfileId, CancellationToken.None);

        if (profile is null)
        {
            return BadRequest("Profile does not exist");
        }

        var card = await _cardService.GetByProfileId(request.ProfileId, CancellationToken.None);

        if (card is not null)
        {
            return BadRequest("Profile already has a card");
        }

        card = _mapper.Map<Card>(request);

        if (!IsCardDataValid(card))
        {
            return BadRequest("Invalid data");
        }

        await _cardService.Create(card, CancellationToken.None);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateCardRequest request)
    {
        var profile = await _profileService.Get(request.ProfileId, CancellationToken.None);

        if (profile is null)
        {
            return BadRequest("Profile does not exist");
        }

        var card = await _cardService.Get(id, CancellationToken.None);

        if (card is null)
        {
            return NotFound();
        }

        var newCard = _mapper.Map<Card>(request);
        newCard.Id = id;
        newCard.Total = card.Total;
        newCard.Code = card.Code;

        if (!IsCardDataValid(newCard))
        {
            return BadRequest("Invalid data");
        }

        await _cardService.Update(newCard, CancellationToken.None);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _cardService.Delete(id, CancellationToken.None);

        return Ok();
    }

    private bool IsCardDataValid(Card card)
    {
        return !(card.Code.Length > 50 || card.Discount > 100 || card.Discount < 0);
    }
}
