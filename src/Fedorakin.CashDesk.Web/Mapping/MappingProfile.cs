using Fedorakin.CashDesk.Logic.Models;
using Fedorakin.CashDesk.Web.Contracts.Requests.Card;
using Fedorakin.CashDesk.Web.Contracts.Requests.Product;
using Fedorakin.CashDesk.Web.Contracts.Requests.Profile;
using Fedorakin.CashDesk.Web.Contracts.Requests.Role;
using Fedorakin.CashDesk.Web.Contracts.Requests.SelfCheckout;
using Fedorakin.CashDesk.Web.Contracts.Requests.Stock;
using Fedorakin.CashDesk.Web.Contracts.Responses;

namespace Fedorakin.CashDesk.Web.Mapping;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        #region DateTime to string
        CreateMap<string, DateTime>()
            .ConvertUsing((x, res) => res = DateTime.TryParse(x, out var dateTime) ? dateTime : DateTime.MinValue);

        CreateMap<DateTime, string>()
            .ConvertUsing((x, res) => res = x.ToString());
        #endregion

        #region Role
        CreateMap<CreateRoleRequest, Role>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<UpdateRoleRequest, Role>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<Role, RoleResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        #endregion

        #region Profile
        CreateMap<Profile, ProfileResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

        CreateMap<CreateProfileRequest, Profile>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));

        CreateMap<UpdateProfileRequest, Profile>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
        #endregion

        #region Self checkout
        CreateMap<CreateSelfCheckoutRequest, SelfCheckout>()
            .ForMember(x => x.IsActive, opt => opt.MapFrom(src => src.IsActive));

        CreateMap<UpdateSelfCheckoutRequest, SelfCheckout>()
            .ForMember(x => x.IsActive, opt => opt.MapFrom(src => src.IsActive));

        CreateMap<SelfCheckout, SelfCheckoutResponse>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(x => x.IsBusy, opt => opt.MapFrom(src => src.IsBusy))
            .ForMember(x => x.IsActive, opt => opt.MapFrom(src => src.IsActive));
        #endregion

        #region Product
        CreateMap<CreateProductRequest, Product>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Barcode, opt => opt.MapFrom(src => src.Barcode))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<UpdateProductRequest, Product>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Barcode, opt => opt.MapFrom(src => src.Barcode))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Barcode, opt => opt.MapFrom(src => src.Barcode))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        #endregion

        #region Stock
        CreateMap<CreateStockRequest, Stock>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count));

        CreateMap<UpdateStockRequest, Stock>()
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count));

        CreateMap<Stock, StockResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count));
        #endregion

        #region Cart
        CreateMap<Cart, CartResponse>()
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        #endregion

        #region Check
        CreateMap<Check, CheckResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.SelfCheckout, opt => opt.MapFrom(src => src.SelfCheckout))
            .ForMember(dest => dest.Card, opt => opt.MapFrom(src => src.Card))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount));
        #endregion

        #region Card
        CreateMap<CreateCardRequest, Card>()
            .ForMember(dest => dest.ProfileId, opt => opt.MapFrom(src => src.ProfileId))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount));

        CreateMap<UpdateCardRequest, Card>()
            .ForMember(dest => dest.ProfileId, opt => opt.MapFrom(src => src.ProfileId))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount));

        CreateMap<Card, CardResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.Profile, opt => opt.MapFrom(src => src.Profile));
        #endregion
    }
}
