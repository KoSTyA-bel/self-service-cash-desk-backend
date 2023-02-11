using AutoMapper;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Web.Contracts.Requests.Product;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Fedorakin.CashDesk.Web.Exceptions;
using FluentValidation;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductManager _productManager;
    private readonly IDataStateManager _dataStateManager;
    private readonly IValidator<Product> _productValidator;
    private readonly IMapper _mapper;

    public ProductController(IProductManager productManager, IDataStateManager dataStateManager, IValidator<Product> productValidator, IMapper mapper)
    {
        _productManager = productManager ?? throw new ArgumentNullException(nameof(productManager));
        _dataStateManager = dataStateManager ?? throw new ArgumentNullException(nameof(dataStateManager));
        _productValidator = productValidator ?? throw new ArgumentNullException(nameof(productValidator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> Get(int page, int pageSize, string? name, string? barcode)
    {
        if (page < 1)
        {
            throw new InvalidPageNumberException();
        }

        if (pageSize < 1)
        {
            throw new InvalidPageSizeException();
        }
        
        name = name ?? string.Empty;
        barcode = barcode ?? string.Empty;

        var products = await _productManager.GetRangeAsync(page, pageSize, name, barcode);

        if (products.Count == 0)
        {
            throw new ElementNotFoundException();
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
            throw new ElementNotFoundException();
        }

        var response = _mapper.Map<ProductResponse>(product);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProductRequest request)
    {
        var product = _mapper.Map<Product>(request);

        _productValidator.ValidateAndThrow(product);

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
            throw new ElementNotFoundException();
        }

        product = _mapper.Map<Product>(request);
        product.Id = id;

        _productValidator.ValidateAndThrow(product);

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
}
