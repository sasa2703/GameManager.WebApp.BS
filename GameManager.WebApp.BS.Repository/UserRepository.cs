using Microsoft.EntityFrameworkCore;
using GameManager.WebApp.BS.Contracts;
using GameManager.WebApp.BS.Entities.Models;
using GameManager.WebApp.BS.Shared.RequestFeatures;
using System.Linq;

namespace GameManager.WebApp.BS.Repository
{
    internal sealed class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public async Task<PagedList<User>> GetAllUsersAsync(UserParameters userParameters, bool trackChanges)
        {
            var users = FindAll(trackChanges)
                .Where(u => u.Deleted == false)
                .Include(u => u.Status)
                .Include(u => u.Role)
                .Include(u => u.UserCategoryNavigation)
                .Include(u => u.Subscription)
                .OrderBy(u => u.Username);


            return await PagedList<User>
                .ToPageListAsync(users, userParameters.PageNumber, userParameters.PageSize);
        }

        public async Task<PagedList<User>> GetAllUsersForSubscriptionAsync(UserParameters userParameters, bool trackChanges)
        {
            var users = FindAll(trackChanges)
                .Where(u => u.Deleted == false)
                .Include(u => u.Status)
                .Include(u => u.Role)
                .Include(u => u.UserCategoryNavigation)
                .Include(u => u.Subscription)
                .Where(x => x.SubscriptionId.StartsWith(userParameters.SubscriptionId))
                .OrderBy(u => u.Username);


            return await PagedList<User>
                .ToPageListAsync(users, userParameters.PageNumber, userParameters.PageSize);
        }

        public async Task AddUserAsync(User user) => Create(user);

        public async Task<User> GetUserAsync(int userId, bool trackChanges) =>
            await FindByCondition(u => u.UserId.Equals(userId) && u.Deleted == false, trackChanges)
            .Include(u => u.Status)
            .Include(u => u.Role)
            .Include(u => u.UserCategoryNavigation)
            .Include(u => u.Subscription)
            .SingleOrDefaultAsync();

        public async Task<User> GetUserByUsernameAsync(string username, bool trackChanges) =>
            await FindByCondition(u => u.Username.Equals(username), trackChanges)
            .Include(u => u.Status)
            .Include(u => u.Role)
            .Include(u => u.UserCategoryNavigation)
            .Include(u => u.Subscription)
            .SingleOrDefaultAsync();

        public async Task<User> GetUserEnableDisableAsync(int userId, bool trackChanges) =>
            await FindByCondition(u => u.UserId.Equals(userId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task DeleteUserAsync(string username)
        {
            var user = await FindByCondition(x => x.Username == username && !x.Deleted, true).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Deleted = true;
            }
        }

        public async Task ReactivateDeleteUserAsync(string username)
        {
            var user = await FindByCondition(x => x.Username == username && x.Deleted, true).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Deleted = false;
            }
        }

        public async Task<int> GetUsersNumberBySubscriptionIdAsync(string subscriptionId, bool trackChanges) =>
            await FindByCondition(x => x.SubscriptionId.Equals(subscriptionId), trackChanges).CountAsync();
    }
}
