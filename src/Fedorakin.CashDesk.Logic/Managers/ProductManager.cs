﻿using Fedorakin.CashDesk.Data.Contexts;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Constants;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace Fedorakin.CashDesk.Logic.Managers;

public class ProductManager : IProductManager
{
    private readonly DataContext _context;

    public ProductManager(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task AddAsync(Product model)
    {
        return _context.Products.AddAsync(model).AsTask();
    }

    public Task DeleteAsync(Product model)
    {
        return Task.FromResult(_context.Products.Remove(model));
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var products = await GetRangeAsync(readIds: new ReadOnlyCollection<int>(new List<int> { id }));

        return products.SingleOrDefault();
    }

    public Task<List<Product>> GetRangeAsync(
        int? page = default, 
        int? pageSize = default, 
        IReadOnlyCollection<int>? readIds = default, 
        IReadOnlyCollection<string>? readNames = default, 
        IReadOnlyCollection<string>? readBarcode = default, 
        params string[] includes)
    {
        var query = _context.Products.AsNoTracking();

        if (readIds is not null && readIds.Any())
        {
            query = query.Where(product => readIds!.Contains(product.Id));
        }

        if (readNames is not null && readNames.Any())
        {
            query = query.Where(product => product.Name.Contains(readNames.First()));
        }

        if (readBarcode is not null && readBarcode.Any())
        {
            query = query.Where(product => product.Barcode.Contains(readBarcode.First()));
        }

        if (page.HasValue && pageSize.HasValue)
        {
            query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        return query.ToListAsync();
    }

    public async Task UpdateAsync(Product model)
    {
        var product = await GetByIdAsync(model.Id);

        if (product is null)
        {
            return;
        }

        product.Name = model.Name;
        product.Price = model.Price;
        product.Description = model.Description;
        product.Weight = model.Weight;
        product.Barcode = model.Barcode;

        _context.Products.Update(product);
    }
}
