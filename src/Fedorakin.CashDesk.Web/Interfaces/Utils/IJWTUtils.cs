using Fedorakin.CashDesk.Web.Settings;

namespace Fedorakin.CashDesk.Web.Interfaces.Utils;

public interface IJWTUtils
{
    public string GenerateToken(JWTSettings.Admin admin);

    public JWTSettings.Admin? ValidateToken(string token);
}
