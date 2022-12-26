using Fedorakin.CashDesk.Logic.Models;
using Fedorakin.CashDesk.Web.View;

namespace Fedorakin.CashDesk.Web;

public class MappingProfile : AutoMapper.Profile
{
	public MappingProfile()
	{
        CreateMap<string, DateTime>()
            .ConvertUsing((x, res) => res = DateTime.TryParse(x, out var dateTime) ? dateTime : DateTime.MinValue);
        CreateMap<DateTime, string>()
            .ConvertUsing((x, res) => res = x.ToString());

        CreateMap<Role, RoleView>()
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
	}
}
