using AutoMapper;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Web.Contracts.Requests.Card;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Fedorakin.CashDesk.Web.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardController : ControllerBase
{
    private readonly ICardManager _cardManager;
    private readonly IProfileManager _profileService;
    private readonly IDataStateManager _dataStateManager;
    private readonly IValidator<CreateCardRequest> _createCardRequestValidator;
    private readonly IValidator<UpdateCardRequest> _updateCardRequestValidator;
    private readonly IMapper _mapper;

    public CardController(
        ICardManager cardManager, 
        IProfileManager profileService, 
        IDataStateManager dataStateManager, 
        IValidator<CreateCardRequest> createCardRequestValidator, 
        IValidator<UpdateCardRequest> updateCardRequestValidator, 
        IMapper mapper)
    {
        _cardManager = cardManager ?? throw new ArgumentNullException(nameof(cardManager));
        _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
        _dataStateManager = dataStateManager ?? throw new ArgumentNullException(nameof(dataStateManager));
        _createCardRequestValidator = createCardRequestValidator ?? throw new ArgumentNullException(nameof(createCardRequestValidator));
        _updateCardRequestValidator = updateCardRequestValidator ?? throw new ArgumentNullException(nameof(updateCardRequestValidator));
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

        var cards = await _cardManager.GetRangeAsync(page, pageSize);

        if (cards.Count == 0)
        {
            throw new ElementNotFoundException();
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
            throw new ElementNotFoundException();
        }

        var response = _mapper.Map<CardResponse>(card);

        return Ok(response);
    }

    [HttpGet("ByCode/{code}")]
    public async Task<IActionResult> Get(string code)
    {
        var card = await _cardManager.GetByCodeAsync(code);

        if (card is null)
        {
            throw new ElementNotFoundException();
        }

        var response = _mapper.Map<CardResponse>(card);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCardRequest request)
    {
        _createCardRequestValidator.ValidateAndThrow(request);

        var profile = await _profileService.GetByIdAsync(request.ProfileId);

        if (profile is null)
        {
            throw new ElementNotFoundException("Profile does not exist");
        }

        var card = await _cardManager.GetByProfileIdAsync(request.ProfileId);

        if (card is not null)
        {
            return BadRequest("Profile already has a card");
        }

        card = _mapper.Map<Card>(request);

        await _cardManager.AddAsync(card);

        await _dataStateManager.CommitChangesAsync();

        return Ok(card.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateCardRequest request)
    {
        _updateCardRequestValidator.ValidateAndThrow(request);

        var profile = await _profileService.GetByIdAsync(request.ProfileId);

        if (profile is null)
        {
            throw new ElementNotFoundException("Profile does not exist");
        }

        var card = await _cardManager.GetByIdAsync(id);

        if (card is null)
        {
            throw new ElementNotFoundException();
        }

        var newCard = _mapper.Map<Card>(request);
        newCard.Id = id;
        newCard.Total = card.Total;
        newCard.Code = card.Code;

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
}
