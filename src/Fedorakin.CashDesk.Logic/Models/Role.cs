using Fedorakin.CashDesk.Logic.Interfaces;

namespace Fedorakin.CashDesk.Logic.Models;

public class Role : BaseEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
