using AutoMapper;
using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Shared.DataTransferObjects.ApiAccessToken;

namespace GameManager.WebApp.BS.API.Mapping
{
    public class ApiAccessTokenProfile : Profile
    {
        public ApiAccessTokenProfile()
        {

            CreateMap<ApiAccessToken, ApiAccessTokenDto>()
               .ForMember(dest => dest.ApiAccessTokenId, opt => opt.MapFrom(src => src.ApiAccessTokenId))
               .ForMember(dest => dest.LoginId, opt => opt.MapFrom(src => src.LoginId))
               .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.DtCreated))
               .ForMember(dest => dest.ExpireDate, opt => opt.MapFrom(src => src.DtExpireDate))
               .ForMember(dest => dest.KeyVaultSecretId, opt => opt.MapFrom(src => src.KeyVaultSecretId));


            CreateMap<ApiAccessTokenForCreationDto, ApiAccessToken>()
                .ForMember(dest => dest.LoginId, opt => opt.MapFrom(src => src.LoginId))
                .ForMember(dest => dest.DtExpireDate, opt => opt.MapFrom(src => src.ExpireDate));
                  
        }
    }
}
