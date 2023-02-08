using Fedorakin.CashDesk.Data.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Services;

public interface ICartService
{
    void SetDateTime(Cart cart);

    void AddProduct(Cart cart, Product product);
}
