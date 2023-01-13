using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Providers;

public interface ICartProvider : IBaseProvider<Cart>
{
    public Task<Cart?> GetCartByNumber(Guid number, CancellationToken cancellationToken);
}
