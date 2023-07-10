using AutoMapper;
using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Game;
using GameManager.WebApp.BS.Shared.DataTransferObjects.GameCollection;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Product;

namespace GameManager.WebApp.BS.Mapping
{
    public class GameCollectionProfiles : Profile
    {
        public GameCollectionProfiles()
        {
            CreateMap<GameCollection, GameCollectionDto>()
                   .ForMember(dest => dest.GameCollectionId, opt => opt.MapFrom(src => src.Id))
                   .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                   .ForMember(dest => dest.DisplayIndex, opt => opt.MapFrom(src => src.DisplayIndex))
                   .ForMember(dest => dest.Games, opt => opt.MapFrom(src => src.Games))
                   .ForMember(dest => dest.GameSubCollections, opt => opt.MapFrom(src => src.GameSubCollections))
                   .ReverseMap();

            CreateMap<EditGameCollectionDto, GameCollection>()
                    .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                    .ForMember(dest => dest.DisplayIndex, opt => opt.MapFrom(src => src.DisplayIndex))
                    .ForMember(dest => dest.Games, opt => opt.MapFrom(src => src.Games))
                    .ForMember(dest => dest.GameSubCollections, opt => opt.MapFrom(src => src.GameSubCollections))
                    .ReverseMap();

            CreateMap<GameCollectionForCreationDto, GameCollection>()
                    .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                    .ForMember(dest => dest.DisplayIndex, opt => opt.MapFrom(src => src.DisplayIndex))
                    .ForMember(dest => dest.Games, opt => opt.MapFrom(src => src.GamesIds))
                    .ForMember(dest => dest.GameSubCollections, opt => opt.MapFrom(src => src.GameSubCollectionsIds))
                    .ReverseMap();
        }
    }
}
