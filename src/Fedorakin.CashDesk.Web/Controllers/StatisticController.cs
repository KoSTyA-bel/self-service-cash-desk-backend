using AutoMapper;
using Fedorakin.CashDesk.Logic.Constants;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Web.Contracts.Requests.Statistic;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatisticController : ControllerBase
{
    private readonly IStatisticService _statisticService;
    private readonly ICheckManager _checkManager;
    private readonly ICartManager _cartManager;
    private readonly IValidator<StatisticRequest> _requesValidator;
    private readonly IMapper _mapper;

    public StatisticController(IStatisticService statisticService, ICheckManager checkManager, ICartManager cartManager, IValidator<StatisticRequest> requestValidator, IMapper mapper)
    {
        _statisticService = statisticService ?? throw new ArgumentNullException(nameof(statisticService));
        _checkManager = checkManager ?? throw new ArgumentNullException(nameof(checkManager));
        _cartManager = cartManager ?? throw new ArgumentNullException(nameof(cartManager));
        _requesValidator = requestValidator ?? throw new ArgumentNullException(nameof(requestValidator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpPost]
    public async Task<IActionResult> CalculateStatistic([FromBody] StatisticRequest request)
    {
        _requesValidator.ValidateAndThrow(request);

        var checks = await _checkManager.GetRangeAsync(
            readCardCodes: new ReadOnlyCollection<string>(new List<string> { request.Code }),
            readCardCVVs: new ReadOnlyCollection<string>(new List<string> { request.Cvv }));

        var cartNumbers = checks.Select(x => x.CartNumber).ToList();

        var carts = await _cartManager.GetRangeAsync(
            readOnlyNumbers: new ReadOnlyCollection<Guid>(cartNumbers),
            includes: IncludeModels.CartNavigation.Products);

        var statistic = _statisticService.CalculateStatistic(checks, carts);

        var response = _mapper.Map<StatisticResponse>(statistic);

        return Ok(response);
    }
}
