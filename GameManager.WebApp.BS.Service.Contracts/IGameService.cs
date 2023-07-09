using GameManager.WebApp.BS.Shared.DataTransferObjects.Product;
using GameManager.WebApp.BS.Shared.RequestFeatures;

namespace GameManager.WebApp.BS.Service.Contracts
{
    public interface IGameService
    {
        Task DeleteGameAsync(int productId);
        Task<List<GameDto>> GetPubliclyAvailableGames(bool trackChanges);
        Task<(IEnumerable<GameDto> games, MetaData metaData)> GetAllGamesAsync(GameParameters gameParameters, bool trackChanges);
        Task<GameDto> GetGameAsync(int gameId, bool trackChanges);
        Task<GameDto> GetGameAsync(string gameIndex, bool trackChanges);
        Task<IEnumerable<GameDto>> GetAllGamesBySubscriptionAsync(string subscriptionId,bool trackChanges);
        Task<GameDto> EditGamesAsync(int gameId, EditGameDto product);
    }
}
