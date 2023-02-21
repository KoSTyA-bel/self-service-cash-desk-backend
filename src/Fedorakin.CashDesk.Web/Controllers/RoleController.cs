using AutoMapper;
using Fedorakin.CashDesk.Data.Models;
using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Web.Attributes;
using Fedorakin.CashDesk.Web.Contracts.Requests.Role;
using Fedorakin.CashDesk.Web.Contracts.Responses;
using Fedorakin.CashDesk.Web.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RoleController : ControllerBase
{
    private readonly IRoleManager _roleManager;
    private readonly IDataStateManager _dataStateManager;
    private readonly IValidator<CreateRoleRequest> _createRoleValidator;
    private readonly IValidator<UpdateRoleRequest> _updateRoleValidator;
    private readonly IMapper _mapper;

    public RoleController(
        IRoleManager roleManager, 
        IDataStateManager dataStateManager, 
        IValidator<CreateRoleRequest> createRoleValidator, 
        IValidator<UpdateRoleRequest> updateRoleValidator, 
        IMapper mapper)
    {
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _dataStateManager = dataStateManager ?? throw new ArgumentNullException(nameof(dataStateManager));
        _createRoleValidator = createRoleValidator ?? throw new ArgumentNullException(nameof(createRoleValidator));
        _updateRoleValidator = updateRoleValidator ?? throw new ArgumentNullException(nameof(updateRoleValidator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> Get(int page, int pageSize)
    {
        if (page < 1)
        {
            throw new InvalidPageNumberException();
        }

        if (pageSize < 1)
        {
            throw new InvalidPageSizeException();
        }

        var roles = await _roleManager.GetRangeAsync(page, pageSize);

        if (roles.Count == 0)
        {
            throw new ElementNotFoundException();
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
            throw new ElementNotFoundException();
        }

        var response = _mapper.Map<RoleResponse>(role);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateRoleRequest request)
    {
        _createRoleValidator.ValidateAndThrow(request);

        var role = _mapper.Map<Role>(request);        

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
            throw new ElementNotFoundException();
        }

        _updateRoleValidator.ValidateAndThrow(request);

        role = _mapper.Map<Role>(request);
        role.Id = id;        

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
}
