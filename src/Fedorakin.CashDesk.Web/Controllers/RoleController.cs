using AutoMapper;
using Azure.Core;
using Fedorakin.CashDesk.Logic.Interfaces.Services;
using Fedorakin.CashDesk.Logic.Models;
using Fedorakin.CashDesk.Web.Contracts.Requests.Role;
using Fedorakin.CashDesk.Web.Contracts.Responses;
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

        var roles = await _service.GetRange(page, pageSize, CancellationToken.None);

        if (roles.Count == 0)
        {
            return NotFound();
        }

        var response = _mapper.Map<List<RoleResponse>>(roles);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var role = await _service.Get(id, CancellationToken.None);

        if (role is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<RoleResponse>(role);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateRoleRequest request)
    {
        var role = _mapper.Map<Role>(request);
        
        if (!IsRoleDataValid(role))
        {
            return BadRequest("Invalid data");
        }

        await _service.Create(role, CancellationToken.None);

        return Ok(role.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateRoleRequest request)
    {
        var role = await _service.Get(id, CancellationToken.None);

        if (role is null)
        {
            return NotFound();
        }

        role = _mapper.Map<Role>(request);
        role.Id = id;

        if (!IsRoleDataValid(role))
        {
            return BadRequest("Invalid data");
        }

        await _service.Update(role, CancellationToken.None);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id, CancellationToken.None);

        return Ok();
    }

    private bool IsRoleDataValid(Role role)
    {
        return !(role.Name.Length > 50);
    }
}
