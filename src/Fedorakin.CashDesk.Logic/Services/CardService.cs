using Fedorakin.CashDesk.Logic.Interfaces;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Services;

public class CardService : ServiceBase<Card>, ICardService
{
    public CardService(ICardProvider provider, ICardRepository repository, IUnitOfWork unitOfWork) 
        : base(provider, repository, unitOfWork)
    {
    }
}
