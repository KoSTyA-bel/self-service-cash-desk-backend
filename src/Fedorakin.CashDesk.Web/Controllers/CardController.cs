using AutoMapper;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Web.Contracts.Requests.Card;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardController : ControllerBase
{
    private readonly ICardManager _cardManager;
    private readonly IProfileManager _profileService;
    private readonly IDataStateManager _dataStateManager;
    private readonly IMapper _mapper;

    public CardController(ICardManager cardManager, IProfileManager profileService, IDataStateManager dataStateManager, IMapper mapper)
    {
        _cardManager = cardManager ?? throw new ArgumentNullException(nameof(cardManager));
        _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
        _dataStateManager = dataStateManager ?? throw new ArgumentNullException(nameof(dataStateManager));
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

        var cards = await _cardManager.GetRangeAsync(page, pageSize);

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
        var card = await _cardManager.GetByIdAsync(id);

        if (card is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<CardResponse>(card);

        return Ok(response);
    }

    //[HttpGet("GetByCode/{code}")]
    //public async Task<IActionResult> Get(string code)
    //{
    //    var card = await _cardManager.GetCardByCode(code, CancellationToken.None);

    //    if (card is null)
    //    {
    //        return NotFound();
    //    }

    //    var response = _mapper.Map<CardResponse>(card);

    //    return Ok(response);
    //}

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCardRequest request)
    {
        var profile = await _profileService.GetByIdAsync(request.ProfileId);

        if (profile is null)
        {
            return BadRequest("Profile does not exist");
        }

        var card = await _cardManager.GetByProfileIdAsync(request.ProfileId);

        if (card is not null)
        {
            return BadRequest("Profile already has a card");
        }

        card = _mapper.Map<Card>(request);

        if (!IsCardDataValid(card))
        {
            return BadRequest("Invalid data");
        }

        await _cardManager.AddAsync(card);

        await _dataStateManager.CommitChangesAsync();

        return Ok(card.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateCardRequest request)
    {
        var profile = await _profileService.GetByIdAsync(request.ProfileId);

        if (profile is null)
        {
            return BadRequest("Profile does not exist");
        }

        var card = await _cardManager.GetByIdAsync(id);

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

        await _cardManager.UpdateAsync(newCard);

        await _dataStateManager.CommitChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var card = await _cardManager.GetByIdAsync(id);

        if (card is not null)
        {
            await _cardManager.DeleteAsync(card);

            await _dataStateManager.CommitChangesAsync();
        }

        return Ok();
    }

    private bool IsCardDataValid(Card card)
    {
        return !(card.Code.Length > 50 || card.Discount > 100 || card.Discount < 0);
    }
}
