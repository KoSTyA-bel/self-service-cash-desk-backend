using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Interfaces.Services;

namespace Fedorakin.CashDesk.Logic.Services;

public class CartService : ICartService
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public CartService(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    public void AddProduct(Cart cart, Product product)
    {
        cart.Price += product.Price;

        cart.Products.Add(product);
    }

    public void RemoveProduct(Cart cart, int productId)
    {
        var product = cart.Products.FirstOrDefault(x => x.Id == productId);

        if (product is not null)
        {
            cart.Products.Remove(product);
            cart.Price -= product.Price;
        }
    }

    public void SetDateTime(Cart cart)
    {
        cart.Date = _dateTimeProvider.Now();
    }
}
