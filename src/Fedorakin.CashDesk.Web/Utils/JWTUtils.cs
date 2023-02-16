using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Web.Interfaces.Utils;
using Fedorakin.CashDesk.Web.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fedorakin.CashDesk.Web.Utils;

public class JWTUtils : IJWTUtils
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JWTSettings _jwtSettings;

    public JWTUtils(IDateTimeProvider dateTimeProvider, JWTSettings jwtSettings)
    {
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
    }

    public string GenerateToken(JWTSettings.Admin admin)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("admin_name", admin.Name.ToString()) }),
            Expires = _dateTimeProvider.NexWeek(),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public JWTSettings.Admin? ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            
            var adminName = jwtToken.Claims.Where(x => x.Type == "admin_name").First().Value;

            var admin = _jwtSettings.Admins.FirstOrDefault(x => x.Name == adminName);

            return admin;
        }
        catch
        {
            return null;
        }
    }
}
