using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Web.Contracts.Requests.Profile;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IProfileManager _profileManager;
    private readonly IRoleManager _roleManager;
    private readonly IDataStateManager _dataStateManager;
    private readonly AutoMapper.IMapper _mapper;

    public ProfileController(IProfileManager profileManager, IRoleManager roleManager, IDataStateManager dataStateManager, AutoMapper.IMapper mapper)
    {
        _profileManager = profileManager ?? throw new ArgumentNullException(nameof(profileManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _dataStateManager = dataStateManager ?? throw new ArgumentNullException(nameof(dataStateManager));
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

        var profiles = await _profileManager.GetRangeAsync(page, pageSize);

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
        var profile = await _profileManager.GetByIdAsync(id);

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

        var role = await _roleManager.GetByIdAsync(profile.RoleId);

        if (role is null)
        {
            return BadRequest("Role does not exist");
        }

        await _profileManager.AddAsync(profile);

        await _dataStateManager.CommitChangesAsync();

        return Ok(profile.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateProfileRequest request)
    {
        var profile = await _profileManager.GetByIdAsync(id);

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

        var role = await _roleManager.GetByIdAsync(profile.RoleId);

        if (role is null)
        {
            return BadRequest("Role does not exist");
        }

        await _profileManager.UpdateAsync(profile);

        await _dataStateManager.CommitChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var profile = await _profileManager.GetByIdAsync(id);

        if (profile is not null)
        {
            await _profileManager.DeleteAsync(profile);
            await _dataStateManager.CommitChangesAsync();
        }

        return Ok();
    }

    private bool IsProfileDataValid(Profile profile)
    {
        return !(profile.FullName.Length > 50);
    }
}
