namespace Fedorakin.CashDesk.Logic.Models;

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public List<Profile> Profiles { get; set; } = new();
}
