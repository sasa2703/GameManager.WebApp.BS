using GameManager.WebApp.BS.Shared.DataTransferObjects.ApiAccessToken;
using GameManager.WebApp.BS.Shared.RequestFeatures;
using System.Security.Claims;

namespace GameManager.WebApp.BS.Service.Contracts
{
    public interface IApiAccessTokenService
    {
        Task<ApiAccessTokenDto> CreateApiAccessTokenAsync(ApiAccessTokenForCreationDto apiAccessToken);
        Task DeleteApiAccessTokenAsync(int id, bool trackChanges);
        Task<ApiAccessTokenDto> GetApiAccessTokenByIdAsync(int id, bool trackChanges);
        Task<(IEnumerable<ApiAccessTokenDto> apiAccessTokenDto, MetaData metaData)> GetApiAccessTokensAsync(ApiAccessTokenParameters apiAccessTokenParam, bool trackChanges, ClaimsPrincipal user);
        Task<ApiAccessTokenDto> GetToken(int id, ClaimsPrincipal user);
    }
}
