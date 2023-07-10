using AutoMapper;
using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Role;

namespace FiscalCloud.WebApp.BS.API.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName))
                .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => src.DtLastUpdate))
                .ForMember(dest => dest.UserCategoryName, opt => opt.MapFrom(src => src.UserCategory.UserCategoryName))
                .ForMember(dest => dest.RoleDescription, opt => opt.MapFrom(src => src.Description));
        }
    }
}
