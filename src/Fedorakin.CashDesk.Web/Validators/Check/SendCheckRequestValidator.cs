using Fedorakin.CashDesk.Web.Contracts.Requests.Check;
using FluentValidation;

namespace Fedorakin.CashDesk.Web.Validators.Check;

public class SendCheckRequestValidator : AbstractValidator<SendCheckRequest>
{
    public SendCheckRequestValidator()
    {
        
    }
}
