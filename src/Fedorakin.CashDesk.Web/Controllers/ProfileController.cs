using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;
using Fedorakin.CashDesk.Web.View;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _service;
    private readonly AutoMapper.IMapper _mapper;

    public ProfileController(IProfileService service, AutoMapper.IMapper mapper)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProfileView>>> Get(int? page, int? pageSize)
    {
        var profiles = await _service.GetRange(page.Value, pageSize.Value, CancellationToken.None);

        if (profiles.Count == 0)
        {
            return NotFound();
        }

        var mappedProfiles = _mapper.Map<List<ProfileView>>(profiles);

        return Ok(mappedProfiles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProfileView>> Get(int id)
    {
        var profile = await _service.Get(id, CancellationToken.None);

        if (profile is null)
        {
            return NotFound();
        }

        var mappedProfile = _mapper.Map<RoleView>(profile);

        return Ok(mappedProfile);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProfileView profileView)
    {
        var profile = _mapper.Map<Profile>(profileView);

        await _service.Create(profile, CancellationToken.None);

        return Ok(profile.Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] ProfileView profileView)
    {
        var profile = _mapper.Map<Profile>(profileView);
        profile.Id = id;

        await _service.Update(profile, CancellationToken.None);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.Delete(id, CancellationToken.None);

        return Ok();
    }
}
