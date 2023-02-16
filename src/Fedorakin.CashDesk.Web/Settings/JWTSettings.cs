namespace Fedorakin.CashDesk.Web.Settings;

public class JWTSettings
{
    public required string Key { get; set; }

    public List<Admin> Admins { get; set; } = new();

    public class Admin
    {
        public required string Name { get; set; }

        public required string Password { get; set; }
    }
}
