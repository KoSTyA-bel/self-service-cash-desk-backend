using AutoMapper;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CheckController : ControllerBase
{
    private readonly ICheckManager _checkManager;
    private readonly IMapper _mapper;

    public CheckController(ICheckManager checkManager, IMapper mapper)
    {
        _checkManager = checkManager ?? throw new ArgumentNullException(nameof(checkManager));
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

        var checks = await _checkManager.GetRangeAsync(page, pageSize);

        if (checks.Count == 0)
        {
            return NotFound();
        }

        var response = _mapper.Map<List<CheckResponse>>(checks);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var check = await _checkManager.GetByIdAsync(id);

        if (check is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<CheckResponse>(check);

        return Ok(response);
    }
}
