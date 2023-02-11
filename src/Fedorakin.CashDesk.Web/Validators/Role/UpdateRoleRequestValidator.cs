using Fedorakin.CashDesk.Web.Contracts.Requests.Role;
using FluentValidation;

namespace Fedorakin.CashDesk.Web.Validators.Role;

public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
{
	public UpdateRoleRequestValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Role name can`t be empty")
			.MaximumLength(50).WithMessage("Role name should be less or equal 50");
	}
}
