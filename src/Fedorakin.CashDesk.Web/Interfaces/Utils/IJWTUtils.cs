using Fedorakin.CashDesk.Web.Models;

namespace Fedorakin.CashDesk.Web.Interfaces.Utils;

public interface IJWTUtils
{
    public string GenerateToken(AdminModel admin);

    public AdminModel ValidateToken(string token);
}
