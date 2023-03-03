namespace Fedorakin.CashDesk.Web.Contracts.Requests.Auth;

public class AuthorizationRequest
{
    public string Name { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
