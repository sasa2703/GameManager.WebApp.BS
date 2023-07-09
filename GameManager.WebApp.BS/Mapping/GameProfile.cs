using AutoMapper;
using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Product;

namespace FiscalCloud.WebApp.BS.API.Mapping
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {

            CreateMap<Game, GameDto>()
               .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
               .ForMember(dest => dest.DisplayIndex, opt => opt.MapFrom(src => src.DisplayIndex))
               .ForMember(dest => dest.ReleaseDateOfGame, opt => opt.MapFrom(src => src.ReleaseDateOfGame))
               .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
               .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail))
               .ForMember(dest => dest.Devices, opt => opt.MapFrom(src => src.Devices));

            CreateMap<EditGameDto, Game>()
               .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
               .ForMember(dest => dest.DisplayIndex, opt => opt.MapFrom(src => src.DisplayIndex))
               .ForMember(dest => dest.ReleaseDateOfGame, opt => opt.MapFrom(src => src.ReleaseDateOfGame))
               .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
               .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail))
               .ForMember(dest => dest.Devices, opt => opt.MapFrom(src => src.Devices));

        }
    }
}
