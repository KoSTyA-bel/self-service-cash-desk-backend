using Fedorakin.CashDesk.Logic.Interfaces;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Services;

public class StockService : ServiceBase<Stock>, IStockService
{
    private IStockProvider _stockProvider;

    public StockService(IStockProvider provider, IStockRepository repository, IUnitOfWork unitOfWork) 
        : base(provider, repository, unitOfWork)
    {
        _stockProvider = provider;
    }

    public Task<Stock?> GetStockForProduct(int productId, CancellationToken cancellationToken)
    {
        return _stockProvider.GetStockForProduct(productId, cancellationToken);
    }
}
