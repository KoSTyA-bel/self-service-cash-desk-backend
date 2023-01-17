using Fedorakin.CashDesk.Logic.Interfaces.Repositories;
using Fedorakin.CashDesk.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Fedorakin.CashDesk.Data.Repositories;

public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
{
    public ProfileRepository(DbSet<Profile> profiles)
        : base(profiles)
    {
    }
}
