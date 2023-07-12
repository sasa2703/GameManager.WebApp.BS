using GameManager.WebApp.BS.Shared.DataTransferObjects.GameCollection;
using GameManager.WebApp.BS.Shared.DataTransferObjects.Product;
using GameManager.WebApp.BS.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager.WebApp.BS.Service.Contracts
{
    public interface IGameCollectionService
    {
        Task<GameCollectionDto> CreateGameCollectionAsync(GameCollectionForCreationDto game);
        Task DeleteGameCollectionAsync(int gameCollectionId);
        Task<GameCollectionDto> EditGameCollectionAsync(int gameCollectionId, EditGameCollectionDto game);
        Task<(IEnumerable<GameCollectionDto> games, MetaData metaData)> GetAllGamesCollectionsAsync(RequestParameters gameParameters, bool trackChanges);
        Task<GameCollectionDto> GetGameCollectionAsync(int gameCollectionId, bool trackChanges);
    }
}
