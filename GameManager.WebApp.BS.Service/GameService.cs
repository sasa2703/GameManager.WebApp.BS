using AutoMapper;
using GameManager.WebApp.BS.Contracts;
using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Service.Contracts;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Game;
using GameManager.WebApp.BS.Shared.DataTransferObjects.GameCollection;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Product;
using GameManager.WebApp.BS.Shared.Exceptions.Game;
using GameManager.WebApp.BS.Shared.RequestFeatures;
using Microsoft.AspNetCore.Http;

namespace GameManager.WebApp.BS.Service
{
    public class GameService : IGameService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GameService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<GameDto> games, MetaData metaData)> GetAllGamesAsync(GameParameters gameParameters, bool trackChanges)
        {
            var gamesWithMetaData = await _repository.Game.GetAllGamesAsync(gameParameters, trackChanges);

            var gamesDto = _mapper.Map<IEnumerable<GameDto>>(gamesWithMetaData);

            return (games: gamesDto, metaData: gamesWithMetaData.MetaData); 
        }

        public async Task<GameDto> GetGameAsync(int gameId, bool trackChanges)
        {
            var product = await _repository.Game.GetGameAsync(gameId, trackChanges);

            if (product is null)
            {
                throw new GameNotFoundException(gameId);
            }

            var productDto = _mapper.Map<GameDto>(product);

            return productDto;
        }

        public async Task<GameDto> CreateGameAsync(GameForCreationDto gameForCreation)
        {
            var gameEntity = _mapper.Map<Game>(gameForCreation);

            _repository.Game.CreateGame(gameEntity);
            await _repository.SaveAsync();

            var game = _mapper.Map<GameDto>(gameEntity);

            return game;
        }

        public async Task DeleteGameAsync(int gameId)
        {
            await _repository.Game.DeleteGameAsync(gameId);           
        }


        public async Task<List<GameDto>> GetPubliclyAvailableGames(bool trackChanges)
        {
            return _mapper.Map<List<GameDto>>(await _repository.Game.GetAllAvailableGames(trackChanges));
        }

        public async Task<GameDto> EditGameAsync(int gameId, EditGameDto game)
        {
            Game existingGame = await _repository.Game.GetGameAsync(gameId, true);

            if(existingGame == null) 
            {
                throw new GameNotFoundException(gameId);
            }

            _mapper.Map(game, existingGame);
            await _repository.SaveAsync();

            return _mapper.Map<GameDto>(existingGame);
        }

        public async Task<GameDto> GetGameAsync(string gameIndex, bool trackChanges)
        {
            var game = await _repository.Game.GetGameAsync(gameIndex, trackChanges);

            if (game is null)
            {
                throw new GameNotFoundException(gameIndex);
            }

            var gameDto = _mapper.Map<GameDto>(game);

            return gameDto;
        }

    }
}
