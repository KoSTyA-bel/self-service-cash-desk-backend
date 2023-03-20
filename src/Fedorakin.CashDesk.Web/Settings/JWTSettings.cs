using Fedorakin.CashDesk.Web.Models;

namespace Fedorakin.CashDesk.Web.Settings;

public class JwtSettings
{
    public required string Key { get; set; }

    public List<AdminModel> Admins { get; set; } = new();
}
