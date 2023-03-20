using Fedorakin.CashDesk.Web.Contracts.Requests.Check;
using FluentValidation;

namespace Fedorakin.CashDesk.Web.Validators.Check;

public class SendCheckRequestValidator : AbstractValidator<SendCheckRequest>
{
    public SendCheckRequestValidator()
    {
        RuleFor(x => x.CheckId)
            .GreaterThan(0);

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Not a mail");
    }
}
