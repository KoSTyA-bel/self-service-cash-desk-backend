using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Interfaces.Services;

public interface ICartService : IBaseService<Cart>
{
    public void AddProductsToCart(Guid cartNumber, List<Product> products);

    public void AddProductToCart(Guid cartNumber, Product product);

    public Task<Cart?> GetCachedCartByNumber(Guid cartNumber, CancellationToken cancellationToken);

    public Task<Cart?> GetCartByNumber(Guid number, CancellationToken cancellationToken);

    public Task<Cart?> TakeCart(Guid cartNumber, CancellationToken cancellationToken);
}
