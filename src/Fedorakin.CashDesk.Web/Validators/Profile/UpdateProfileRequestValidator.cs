using Fedorakin.CashDesk.Web.Contracts.Requests.Profile;
using FluentValidation;

namespace Fedorakin.CashDesk.Web.Validators.Profile;

public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
{
	public UpdateProfileRequestValidator()
	{
		RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name can`t be empty")
            .MaximumLength(50).WithMessage("Full name should ne less or equeal 50");
    }
}
