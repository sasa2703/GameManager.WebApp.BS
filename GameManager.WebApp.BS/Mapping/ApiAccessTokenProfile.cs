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
               .ForMember(dest => dest.ApiAccessTokenId, opt => opt.MapFrom(src => src.IApiAccessTokenId))
               .ForMember(dest => dest.SubscriptionId, opt => opt.MapFrom(src => src.SSubscriptionId))
               .ForMember(dest => dest.SubscriptionName, opt => opt.MapFrom(src => src.SSubscriptionName))
               .ForMember(dest => dest.LoginId, opt => opt.MapFrom(src => src.SLoginId))
               .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.DtCreated))
               .ForMember(dest => dest.ExpireDate, opt => opt.MapFrom(src => src.DtExpireDate))
               .ForMember(dest => dest.Subscription, opt => opt.MapFrom(src => src.SSubscription))
               .ForMember(dest => dest.KeyVaultSecretId, opt => opt.MapFrom(src => src.SKeyVaultSecretId));


            CreateMap<ApiAccessTokenForCreationDto, ApiAccessToken>()
                .ForMember(dest => dest.SSubscriptionId, opt => opt.MapFrom(src => src.SubscriptionId))
                .ForMember(dest => dest.SSubscriptionName, opt => opt.MapFrom(src => src.SubscriptionName))
                .ForMember(dest => dest.SLoginId, opt => opt.MapFrom(src => src.LoginId))
                .ForMember(dest => dest.DtExpireDate, opt => opt.MapFrom(src => src.ExpireDate));
                  
        }
    }
}
