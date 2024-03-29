﻿using AutoMapper;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Constants;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Web.Attributes;
using Fedorakin.CashDesk.Web.Contracts.Requests.Stock;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Fedorakin.CashDesk.Web.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockManager _stockManager;
    private readonly IProductManager _productManager;
    private readonly IDataStateManager _dataStateManager;
    private readonly IValidator<CreateStockRequest> _createStockRequestValidator;
    private readonly IValidator<UpdateStockRequest> _updateStockRequestValidator;
    private readonly IMapper _mapper;

    public StockController(
        IStockManager stockManager, 
        IProductManager productManager, 
        IDataStateManager dataStateManager, 
        IValidator<CreateStockRequest> createStockRequestValidator, 
        IValidator<UpdateStockRequest> updateStockRequestValidator, 
        IMapper mapper)
    {
        _stockManager = stockManager ?? throw new ArgumentNullException(nameof(stockManager));
        _productManager = productManager ?? throw new ArgumentNullException(nameof(productManager));
        _dataStateManager = dataStateManager ?? throw new ArgumentNullException(nameof(dataStateManager));
        _createStockRequestValidator = createStockRequestValidator ?? throw new ArgumentNullException(nameof(createStockRequestValidator));
        _updateStockRequestValidator = updateStockRequestValidator ?? throw new ArgumentNullException(nameof(updateStockRequestValidator));
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

        var stocks = await _stockManager.GetRangeAsync(
            page, 
            pageSize, 
            readNames: new ReadOnlyCollection<string>(new List<string> { name }),
            readBarcodes: new ReadOnlyCollection<string>(new List<string> { barcode }),
            includes: IncludeModels.StockNavigation.Product);

        if (stocks.Count == 0)
        {
            throw new ElementNotFoundException();
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
            throw new ElementNotFoundException();
        }

        var response = _mapper.Map<StockResponse>(stock);

        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] CreateStockRequest request)
    {
        _createStockRequestValidator.ValidateAndThrow(request);

        var product = await _productManager.GetByIdAsync(request.ProductId);

        if (product is null)
        {
            throw new ElementNotFoundException();
        }

        var stock = await _stockManager.GetStockForProductAsync(request.ProductId);

        if (stock is not null)
        {
            throw new StockAlreadyExsistsException();
        }

        stock = _mapper.Map<Stock>(request);

        await _stockManager.AddAsync(stock);

        await _dataStateManager.CommitChangesAsync();

        return Ok(stock.Id);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateStockRequest request)
    {
        var stock = await _stockManager.GetByIdAsync(id);

        if (stock is null)
        {
            throw new ElementNotFoundException();
        }

        _updateStockRequestValidator.ValidateAndThrow(request);

        var mappedStock = _mapper.Map<Stock>(request);
        mappedStock.Id = id;
        mappedStock.ProductId = stock.ProductId;

        await _stockManager.UpdateAsync(mappedStock);

        await _dataStateManager.CommitChangesAsync();

        return Ok(stock.Id);
    }

    [HttpDelete("{id}")]
    [Authorize]
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
