namespace Fedorakin.CashDesk.Logic.Models;

public class Person
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public int RoleId { get; set; }

    public Role? Role { get; set; }
}
