using GameManager.WebApp.BS.Contracts;
using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace GameManager.WebApp.BS.Repository
{
    public class ApiAccessTokenRepository : RepositoryBase<ApiAccessToken>, IApiAccessTokenRepository
    {
        public ApiAccessTokenRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<ApiAccessToken>> GetAllApiAccessTokenAsync(ApiAccessTokenParameters requestParameters, bool trackChanges)
        {
            IQueryable<ApiAccessToken> apiAccessToken = FindAll(trackChanges)
                                                        .Include(s => s.SSubscription);
                                

            if (!string.IsNullOrWhiteSpace(requestParameters.SubscriptionId))
            {
                apiAccessToken = apiAccessToken.Where(x => x.SSubscriptionId.Contains(requestParameters.SubscriptionId));
            }

            var apiAccessTokensOrdered = apiAccessToken.OrderBy(x => x.IApiAccessTokenId);

            return await PagedList<ApiAccessToken>
                .ToPageListAsync(apiAccessTokensOrdered, requestParameters.PageNumber, requestParameters.PageSize);
        }

        public async Task<PagedList<ApiAccessToken>> GetApiAccessTokenForExternalUserAsync(ApiAccessTokenParameters requestParameters, bool trackChanges, string subscriptionId)
        {
            IQueryable<ApiAccessToken> apiAccessToken = FindByCondition(x => x.SSubscriptionId == subscriptionId, trackChanges)
                                                               .Include(s => s.SSubscription);


            if (!string.IsNullOrWhiteSpace(requestParameters.SubscriptionId))
            {
                apiAccessToken = apiAccessToken.Where(x => x.SSubscriptionId.Contains(requestParameters.SubscriptionId));
            }

            var apiAccessTokensOrdered = apiAccessToken.OrderBy(x => x.IApiAccessTokenId);

            return await PagedList<ApiAccessToken>
                .ToPageListAsync(apiAccessTokensOrdered, requestParameters.PageNumber, requestParameters.PageSize);
        }

        public void CreateApiAccessToken(ApiAccessToken apiAccessToken) => Create(apiAccessToken);

        public void DeleteApiAccessToken(ApiAccessToken apiAccessToken) => Delete(apiAccessToken);

        public async Task<ApiAccessToken> GetApiAccessTokenByIdAsync(int id, bool trackChanges) =>
           await FindByCondition(u => u.IApiAccessTokenId == id, trackChanges)
                        .Include(s => s.SSubscription)
                        .SingleOrDefaultAsync();
    
    }
}
