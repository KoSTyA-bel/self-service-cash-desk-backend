using Fedorakin.CashDesk.Logic.Interfaces;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;

namespace Fedorakin.CashDesk.Logic.Services;

public class RoleService : ServiceBase<Role>, IRoleService
{
    public RoleService(IRoleProvider provider, IRoleRepository repository, IUnitOfWork unitOfWork) 
        : base(provider, repository, unitOfWork)
    {
    }
}
