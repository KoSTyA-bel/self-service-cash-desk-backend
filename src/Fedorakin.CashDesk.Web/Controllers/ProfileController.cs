using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;
using Fedorakin.CashDesk.Web.Contracts.Requests.Profile;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;
    private readonly IRoleService _roleService;
    private readonly AutoMapper.IMapper _mapper;

    public ProfileController(IProfileService profileService, IRoleService roleService, AutoMapper.IMapper mapper)
    {
        _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
        _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> Get(int page, int pageSize)
    {
        if (page < 1)
        {
            return BadRequest("Page must be greater than 1");
        }

        if (pageSize < 1)
        {
            return BadRequest("Page size must be greater than 1");
        }

        var profiles = await _profileService.GetRange(page, pageSize, CancellationToken.None);

        if (profiles.Count == 0)
        {
            return NotFound();
        }

        var response = _mapper.Map<List<ProfileResponse>>(profiles);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var profile = await _profileService.Get(id, CancellationToken.None);

        if (profile is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<ProfileResponse>(profile);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProfileRequest request)
    {
        var profile = _mapper.Map<Profile>(request);

        if (!IsProfileDataValid(profile))
        {
            return BadRequest("Invalid data");
        }

        var role = await _roleService.Get(profile.RoleId, CancellationToken.None);

        if (role is null)
        {
            return BadRequest("Role does not exist");
        }

        await _profileService.Create(profile, CancellationToken.None);

        return Ok(profile.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateProfileRequest request)
    {
        var profile = await _profileService.Get(id, CancellationToken.None);

        if (profile is null)
        {
            return NotFound();
        }

        profile = _mapper.Map<Profile>(request);
        profile.Id = id;

        if (!IsProfileDataValid(profile))
        {
            return BadRequest("Invalid data");
        }

        var role = await _roleService.Get(profile.RoleId, CancellationToken.None);

        if (role is null)
        {
            return BadRequest("Role does not exist");
        }

        await _profileService.Update(profile, CancellationToken.None);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _profileService.Delete(id, CancellationToken.None);

        return Ok();
    }

    private bool IsProfileDataValid(Profile profile)
    {
        return !(profile.FullName.Length > 50);
    }
}
