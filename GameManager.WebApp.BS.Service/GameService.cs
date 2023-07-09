using AutoMapper;
using GameManager.WebApp.BS.Contracts;
using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Service.Contracts;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Product;
using GameManager.WebApp.BS.Shared.Exceptions.Game;
using GameManager.WebApp.BS.Shared.RequestFeatures;

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

        public async Task DeleteGameAsync(int gameId)
        {
            await _repository.Game.DeleteGameAsync(gameId);           
        }


        public async Task<IEnumerable<GameDto>> GetAllGamesBySubscriptionAsync(string subscriptionId, bool trackChanges)
        {
            var products = await _repository.Game.GetAllGamesBySubscriptionIdAsync(subscriptionId,trackChanges);

            var productsDto = _mapper.Map<IEnumerable<GameDto>>(products);

            return productsDto;
        }

        public async Task<List<GameDto>> GetPubliclyAvailableGames(bool trackChanges)
        {
            return _mapper.Map<List<GameDto>>(await _repository.Game.GetAllAvailableGames(trackChanges));
        }

        public async Task<GameDto> EditGamesAsync(int gameId, EditGameDto game)
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

        public async Task<GameDto> GetGameAsync(string productCode, bool trackChanges)
        {
            var product = await _repository.Game.GetGameAsync(productCode, trackChanges);

            if (product is null)
            {
                throw new GameNotFoundException(productCode);
            }

            var productDto = _mapper.Map<GameDto>(product);

            return productDto;
        }

    }
}
