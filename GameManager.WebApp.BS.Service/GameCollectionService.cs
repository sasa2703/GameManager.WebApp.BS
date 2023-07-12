using AutoMapper;
using GameManager.WebApp.BS.Contracts;
using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Service.Contracts;
using GameManager.WebApp.BS.Shared.DataTransferObjects.GameCollection;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Product;
using GameManager.WebApp.BS.Shared.Exceptions.Game;
using GameManager.WebApp.BS.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.WebApp.BS.Service
{
    public class GameCollectionService : IGameCollectionService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GameCollectionService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GameCollectionDto> CreateGameCollectionAsync(GameCollectionForCreationDto gameCollectionForCreation)
        {
            var gameEntity = _mapper.Map<GameCollection>(gameCollectionForCreation);

            _repository.GameCollection.CreateGameCollection(gameEntity);
            await _repository.SaveAsync();

            var game = _mapper.Map<GameCollectionDto>(gameEntity);

            return game;
        }

        public async Task DeleteGameCollectionAsync(int gameCollectionId)
        {
            await _repository.GameCollection.DeleteGameCollectionAsync(gameCollectionId);
        }

        public async Task<GameCollectionDto> EditGameCollectionAsync(int gameCollectionId, EditGameCollectionDto game)
        {
            GameCollection existingGame = await _repository.GameCollection.GetGameCollectionAsync(gameCollectionId, true);

            if (existingGame == null)
            {
                throw new GameNotFoundException(gameCollectionId);
            }

            _mapper.Map(game, existingGame);
            await _repository.SaveAsync();

            return _mapper.Map<GameCollectionDto>(existingGame);
        }

        public async Task<(IEnumerable<GameCollectionDto> games, MetaData metaData)> GetAllGamesCollectionsAsync(RequestParameters requestParameters, bool trackChanges)
        {
            var gamesWithMetaData = await _repository.GameCollection.GetAllGamesCollectionAsync(requestParameters, trackChanges);

            var gamesCollectionDto = _mapper.Map<IEnumerable<GameCollectionDto>>(gamesWithMetaData);

            return (gamesCollection: gamesCollectionDto, metaData: gamesWithMetaData.MetaData);
        }

        public async Task<GameCollectionDto> GetGameCollectionAsync(int gameCollectionId, bool trackChanges)
        {
            var gameCollection = await _repository.GameCollection.GetGameCollectionAsync(gameCollectionId, trackChanges);

            if (gameCollection is null)
            {
                throw new GameNotFoundException(gameCollectionId);
            }

            var gameCollectionDto = _mapper.Map<GameCollectionDto>(gameCollection);

            return gameCollectionDto;
        }
    }
}
