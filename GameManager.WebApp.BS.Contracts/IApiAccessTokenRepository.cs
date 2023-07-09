using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Shared.RequestFeatures;

namespace GameManager.WebApp.BS.Contracts
{
    public interface IApiAccessTokenRepository
    {
        void CreateApiAccessToken(ApiAccessToken apiAccessToken);
        void DeleteApiAccessToken(ApiAccessToken apiAccessToken);
        Task<PagedList<ApiAccessToken>> GetAllApiAccessTokenAsync(ApiAccessTokenParameters requestParameters, bool trackChanges);
        Task<ApiAccessToken> GetApiAccessTokenByIdAsync(int id, bool trackChanges);
        Task<PagedList<ApiAccessToken>> GetApiAccessTokenForExternalUserAsync(ApiAccessTokenParameters requestParameters, bool trackChanges, string subscriptionID);
    }
}
