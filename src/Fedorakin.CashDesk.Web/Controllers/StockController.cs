using AutoMapper;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;
using Fedorakin.CashDesk.Web.Contracts.Requests.Stock;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockService _service;
    private readonly IMapper _mapper;

    public StockController(IStockService service, IMapper mapper)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> Get(int? page, int? pageSize)
    {
        var stocks = await _service.GetRange(page.Value, pageSize.Value, CancellationToken.None);

        if (stocks.Count == 0)
        {
            return NotFound();
        }

        var response = _mapper.Map<List<StockResponse>>(stocks);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var stock = await _service.Get(id, CancellationToken.None);

        if (stock is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<StockResponse>(stock);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateStockRequest request)
    {
        var stock = _mapper.Map<Stock>(request);

        await _service.Create(stock, CancellationToken.None);

        return Ok(stock.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateStockRequest request)
    {
        var stock = _mapper.Map<Stock>(request);
        stock.Id = id;

        await _service.Update(stock, CancellationToken.None);

        return Ok(stock.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id, CancellationToken.None);

        return Ok();
    }
}
