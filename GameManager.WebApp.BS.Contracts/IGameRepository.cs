using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Shared.RequestFeatures;

namespace GameManager.WebApp.BS.Contracts
{
    public interface IGameRepository
    {
        Task DeleteGameAsync(int gameId);
        Task<List<Game>> GetAllAvailableGames(bool trackChanges);
        Task<PagedList<Game>> GetAllGamesAsync(GameParameters gameParameters,bool trackChanges);
        Task<Game> GetGameAsync(int gameId, bool trackChanges);
        Task<Game> GetGameAsync(string gameIndex, bool trackChanges);
        void CreateGame(Game gameEntity);
    }
}
