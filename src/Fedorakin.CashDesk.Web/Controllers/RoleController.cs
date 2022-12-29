using AutoMapper;
using Azure.Core;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;
using Fedorakin.CashDesk.Web.Requests.Role;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _service;
    private readonly IMapper _mapper;

    public RoleController(IRoleService service, IMapper mapper)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> Get(int? page, int? pageSize)
    {
        var roles = await _service.GetRange(page.Value, pageSize.Value, CancellationToken.None);

        if (roles.Count == 0)
        {
            return NotFound();
        }

        var mappedRoles = _mapper.Map<List<Role>>(roles);

        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Role>> Get(int id)
    {
        var role = await _service.Get(id, CancellationToken.None);

        if (role is null)
        {
            return NotFound();
        }

        return Ok(role);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateRoleRequest request)
    {
        var role = _mapper.Map<Role>(request);
        
        await _service.Create(role, CancellationToken.None);

        return Ok(role.Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateRoleRequest request)
    {
        var role = _mapper.Map<Role>(request);
        role.Id = id;

        await _service.Update(role, CancellationToken.None);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.Delete(id, CancellationToken.None);

        return Ok();
    }
}
