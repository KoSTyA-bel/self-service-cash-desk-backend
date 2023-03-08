using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Services;

public class StatisticService : IStatisticService
{
    public Statistic CalculateStatistic(IEnumerable<Check> checks, IEnumerable<Cart> carts)
    {
        var totalDiscount = CalculateTotalDiscount(checks);
        var totalAmount = CalculateTotalAmount(checks);
        var total = CalculateTotal(checks);
        var averageCheckPrice = CalculateAverageCheckPrice(checks);
        var ProductsCount = CalculateProductsCount(carts);
        var dictuonary = FindCountOfProducts(carts);

        var statistic = new Statistic
        {
            AveragePrice = averageCheckPrice,
            ProductsCount = dictuonary,
            Total = total,
            TotalAmount = totalAmount,
            TotalDiscount = totalDiscount,
        };

        return statistic;
    }

    private int CalculateProductsCount(IEnumerable<Cart> carts)
    {
        var count = carts.Sum(x => x.Products.Count);

        return count;
    }

    private double CalculateAverageCheckPrice(IEnumerable<Check> checks)
    {
        var average = checks.Average(x => x.Total);

        return average;
    }

    private double CalculateTotal(IEnumerable<Check> checks)
    {
        var sum = checks.Sum((x) => x.Total);

        return sum;
    }

    private double CalculateTotalDiscount(IEnumerable<Check> checks)
    {
        var sum = checks.Sum(x => x.Discount);

        return sum;
    }

    private double CalculateTotalAmount(IEnumerable<Check> checks)
    {
        var sum = checks.Sum(x => x.Amount);

        return sum;
    }

    private Dictionary<Product, int>  FindCountOfProducts(IEnumerable<Cart> carts)
    {
        var dictionary = new Dictionary<Product, int>();

        foreach (var cart in carts)
        {
            foreach (var product in cart.Products)
            {
                if (!dictionary.TryGetValue(product, out var count))
                {
                    dictionary.Add(product, 1);
                }
                else
                {
                    dictionary[product] = count + 1;
                }
            }
        }

        return dictionary;
    }
}
