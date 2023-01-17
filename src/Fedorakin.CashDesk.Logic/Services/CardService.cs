using Fedorakin.CashDesk.Logic.Interfaces;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Services;

public class CardService : ServiceBase<Card>, ICardService
{
    protected readonly new ICardProvider _provider;

    public CardService(ICardProvider provider, ICardRepository repository, IUnitOfWork unitOfWork) 
        : base(provider, repository, unitOfWork)
    {
        _provider = provider ?? throw new ArgumentNullException();
    }

    public Task<Card?> GetByProfileId(int id, CancellationToken cancellationToken)
    {
        return _provider.GetByProfileId(id, cancellationToken);
    }

    public Task<Card?> GetCardByCode(string code, CancellationToken cancellationToken)
    {
        return _provider.GetCardByCode(code, cancellationToken);
    }
}
