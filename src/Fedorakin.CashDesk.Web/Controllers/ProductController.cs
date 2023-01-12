﻿using AutoMapper;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;
using Fedorakin.CashDesk.Web.Contracts.Requests.Product;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    private readonly IMapper _mapper;

    public ProductController(IProductService service, IMapper mapper)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> Get(int? page, int? pageSize)
    {
        var products = await _service.GetRange(page.Value, pageSize.Value, CancellationToken.None);

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
        var product = await _service.Get(id, CancellationToken.None);

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

        await _service.Create(product, CancellationToken.None);

        return Ok(product.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateProductRequest request)
    {
        var product = _mapper.Map<Product>(request);
        product.Id = id;

        await _service.Update(product, CancellationToken.None);

        return Ok(product.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id, CancellationToken.None);

        return Ok();
    }
}
