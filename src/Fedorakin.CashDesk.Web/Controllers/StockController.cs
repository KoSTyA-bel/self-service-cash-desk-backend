using AutoMapper;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Web.Contracts.Requests.Stock;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockManager _stockManager;
    private readonly IDataStateManager _dataStateManager;
    private readonly IMapper _mapper;

    public StockController(IStockManager stockManager, IDataStateManager dataStateManager, IMapper mapper)
    {
        _stockManager = stockManager ?? throw new ArgumentNullException(nameof(stockManager));
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

        var stocks = await _stockManager.GetRangeAsync(page, pageSize);

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
        var stock = await _stockManager.GetByIdAsync(id);

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

        await _stockManager.AddAsync(stock);

        await _dataStateManager.CommitChangesAsync();

        return Ok(stock.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateStockRequest request)
    {
        var stock = await _stockManager.GetByIdAsync(id);

        if (stock is null)
        {
            return NotFound();
        }

        stock = _mapper.Map<Stock>(request);
        stock.Id = id;

        await _stockManager.UpdateAsync(stock);

        await _dataStateManager.CommitChangesAsync();

        return Ok(stock.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var stock = await _stockManager.GetByIdAsync(id);

        if (stock is not null)
        {
            await _stockManager.DeleteAsync(stock);

            await _dataStateManager.CommitChangesAsync();
        }        

        return Ok();
    }
}
