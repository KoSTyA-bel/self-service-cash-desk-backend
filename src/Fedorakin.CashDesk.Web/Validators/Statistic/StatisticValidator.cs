using Fedorakin.CashDesk.Web.Contracts.Requests.Statistic;
using FluentValidation;

namespace Fedorakin.CashDesk.Web.Validators.Statistic;

public class StatisticValidator : AbstractValidator<StatisticRequest>
{
    public StatisticValidator()
    {
        RuleFor(x => x.Code)
            .Length(16).WithMessage("Code length should be equal 16");

        RuleFor(x => x.Cvv)
            .Length(3).WithMessage("Cvv length should be equal 3");
    }
}
