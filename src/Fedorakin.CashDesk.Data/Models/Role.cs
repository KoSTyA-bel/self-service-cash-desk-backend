namespace Fedorakin.CashDesk.Data.Models;

public class Role
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public List<Profile> Profiles { get; set; } = new();
}
