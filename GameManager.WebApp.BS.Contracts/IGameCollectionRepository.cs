using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.WebApp.BS.Contracts
{
    public interface IGameCollectionRepository
    {
        Task<PagedList<GameCollection>> GetAllGamesCollectionAsync(RequestParameters gameParameters, bool trackChanges);
        Task DeleteGameCollectionAsync(int gameCollectionId);
        void CreateGameCollection(GameCollection gameEntity);
        Task<GameCollection> GetGameCollectionAsync(int gameCollectionId, bool v);
    }
}
