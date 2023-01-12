namespace Fedorakin.CashDesk.Web.Contracts.Responses;

public class ProfileResponse
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public RoleResponse Role { get; set; } = null!;
}
