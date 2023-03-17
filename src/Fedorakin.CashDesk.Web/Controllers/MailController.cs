using Fedorakin.CashDesk.Logic.Interfaces.Managers;
using Fedorakin.CashDesk.Web.Contracts.Requests.Check;
using Fedorakin.CashDesk.Web.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Fedorakin.CashDesk.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MailController : ControllerBase
{
    private readonly IMailManager _mailManager;
    private readonly ICheckManager _checkManager;
    private readonly IValidator<SendCheckRequest> _validator;

    public MailController(IMailManager mailManager, ICheckManager checkManager, IValidator<SendCheckRequest> validator)
    {
        _mailManager = mailManager ?? throw new ArgumentNullException(nameof(mailManager));
        _checkManager = checkManager ?? throw new ArgumentNullException(nameof(checkManager));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    [HttpPost]
    public async Task<IActionResult> SendCheck([FromBody] SendCheckRequest request)
    {
        _validator.ValidateAndThrow(request);

        var check = await _checkManager.GetByIdAsync(request.CheckId);

        if (check is null)
        {
            throw new ElementNotFoundException();
        }

        await _mailManager.SendCheck(request.Email, check);

        return Ok();
    }
}
