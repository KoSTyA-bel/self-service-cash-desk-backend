namespace Fedorakin.CashDesk.Logic.Models;

public class Profile : BaseEntity
{
    public string FullName { get; set; } = string.Empty;

    public int RoleId { get; set; }

    public Role? Role { get; set; }
}
