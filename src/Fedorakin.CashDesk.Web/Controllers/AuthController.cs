using AutoMapper;
using Fedorakin.CashDesk.Logic.Interfaces.Providers;
using Fedorakin.CashDesk.Web.Contracts.Requests.Auth;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Fedorakin.CashDesk.Web.Exceptions;
using Fedorakin.CashDesk.Web.Models;
using Fedorakin.CashDesk.Web.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;

    public AuthController(
        IMapper mapper,
        IDateTimeProvider dateTimeProvider,
        JwtSettings jwtSettings)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
    }

    [HttpPost]
    public IActionResult GetToken([FromBody] AuthorizationRequest request)
    {
        var admin = _mapper.Map<AdminModel>(request);

        var token = GenerateJwtToken(admin);

        var response = new AuthorizationResponse
        {
            Token = token,
        };

        return Ok(response);
    }

    [NonAction]
    private string GenerateJwtToken(AdminModel admin)
    {
        if (!_jwtSettings.Admins.Any(x => x.Name == admin.Name && x.Password == admin.Password))
        {
            throw new ElementNotFoundException();
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(JwtRegisteredClaimNames.UniqueName, admin.Name.ToString()) }),
            Expires = _dateTimeProvider.NexWeek(),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
