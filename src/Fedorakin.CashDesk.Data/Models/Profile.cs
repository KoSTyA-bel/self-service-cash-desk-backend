namespace Fedorakin.CashDesk.Data.Models;

public class Profile
{
    public int Id { get; set; }

    public required string FullName { get; set; }

    public int RoleId { get; set; }

    public Role? Role { get; set; }
}
