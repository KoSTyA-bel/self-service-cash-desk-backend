namespace Fedorakin.CashDesk.Web.Requests.Profile;

public class UpdateProfileRequest
{
    public string FullName { get; set; } = string.Empty;

    public int RoleId { get; set; }
}
