using Fedorakin.CashDesk.Data.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Fedorakin.CashDesk.Logic.Interfaces.Services;

public interface ICacheService
{
    void SetCart(Cart cart);

    void RemoveCart(Guid number);

    void SetSelfCheckout(SelfCheckout selfCheckout);

    void RemoveSelfCheckout(int id);

    bool TryGetCart(Guid number, out Cart? cart);

    bool TryGetSelfCheckout(int id, out SelfCheckout? cart);
}
