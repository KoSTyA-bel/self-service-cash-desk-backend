using Fedorakin.CashDesk.Logic.Models;
using Fedorakin.CashDesk.Web.Requests.Profile;
using Fedorakin.CashDesk.Web.Requests.Role;

namespace Fedorakin.CashDesk.Web;

public class MappingProfile : AutoMapper.Profile
{
	public MappingProfile()
	{
        CreateMap<string, DateTime>()
            .ConvertUsing((x, res) => res = DateTime.TryParse(x, out var dateTime) ? dateTime : DateTime.MinValue);
        CreateMap<DateTime, string>()
            .ConvertUsing((x, res) => res = x.ToString());

        CreateMap<CreateRoleRequest, Role>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        CreateMap<UpdateRoleRequest, Role>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<CreateProfileRequest, Profile>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
        CreateMap<UpdateProfileRequest, Profile>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
    }
}
