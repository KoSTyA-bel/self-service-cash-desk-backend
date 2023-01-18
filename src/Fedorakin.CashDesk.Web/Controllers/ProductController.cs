using AutoMapper;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Web.Contracts.Requests.Product;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductManager _productManager;
    private readonly IDataStateManager _dataStateManager;
    private readonly IMapper _mapper;

    public ProductController(IProductManager productManager, IDataStateManager dataStateManager, IMapper mapper)
    {
        _productManager = productManager ?? throw new ArgumentNullException(nameof(productManager));
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

        var products = await _productManager.GetRangeAsync(page, pageSize);

        if (products.Count == 0)
        {
            return NotFound();
        }

        var response = _mapper.Map<List<ProductResponse>>(products);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _productManager.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<ProductResponse>(product);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProductRequest request)
    {
        var product = _mapper.Map<Product>(request);

        if (!IsProductDataValid(product))
        {
            return BadRequest("Invalid data");
        }

        await _productManager.AddAsync(product);

        await _dataStateManager.CommitChangesAsync();

        return Ok(product.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateProductRequest request)
    {
        var product = await _productManager.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        product = _mapper.Map<Product>(request);
        product.Id = id;

        if (!IsProductDataValid(product))
        {
            return BadRequest("Invalid data");
        }

        await _productManager.UpdateAsync(product);

        await _dataStateManager.CommitChangesAsync();

        return Ok(product.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productManager.GetByIdAsync(id);

        if (product is not null)
        {
            await _productManager.DeleteAsync(product);

            await _dataStateManager.CommitChangesAsync();
        }

        return Ok();
    }

    private bool IsProductDataValid(Product product)
    {
        return !(product.Name.Length > 50 || product.Description.Length > 50 || product.Description.Length > 50);
    }
}
