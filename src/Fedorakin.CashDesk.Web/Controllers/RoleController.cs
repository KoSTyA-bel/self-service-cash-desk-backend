using AutoMapper;
using Azure.Core;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Web.Contracts.Requests.Role;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleManager _roleManager;
    private readonly IDataStateManager _dataStateManager;
    private readonly IMapper _mapper;

    public RoleController(IRoleManager roleManager, IDataStateManager dataStateManager, IMapper mapper)
    {
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

        var roles = await _roleManager.GetRangeAsync(page, pageSize);

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
        var role = await _roleManager.GetByIdAsync(id);

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

        await _roleManager.AddAsync(role);

        await _dataStateManager.CommitChangesAsync();

        return Ok(role.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateRoleRequest request)
    {
        var role = await _roleManager.GetByIdAsync(id);

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

        await _roleManager.UpdateAsync(role);

        await _dataStateManager.CommitChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var role = await _roleManager.GetByIdAsync(id);

        if (role is not null)
        {
            await _roleManager.DeleteAsync(role);

            await _dataStateManager.CommitChangesAsync();
        }        

        return Ok();
    }

    private bool IsRoleDataValid(Role role)
    {
        return !(role.Name.Length > 50);
    }
}
