using AutoMapper;
using Fedorakin.CashDesk.Web.Contracts.Requests.Auth;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Fedorakin.CashDesk.Web.Interfaces.Utils;
using Fedorakin.CashDesk.Web.Settings;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IJWTUtils _jwtUtils;
    private readonly IMapper _mapper;

    public AuthController(IJWTUtils jwtUtils, IMapper mapper)
    {
        _jwtUtils = jwtUtils ?? throw new ArgumentNullException(nameof(jwtUtils));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpPost]
    public async Task<IActionResult> GetToken([FromBody] AuthorizationRequest request)
    {
        var admin = _mapper.Map<JWTSettings.Admin>(request);

        var token = _jwtUtils.GenerateToken(admin);

        var response = new AuthorizationResponse
        {
            Token = token,
        };

        return Ok(response);
    }
}
