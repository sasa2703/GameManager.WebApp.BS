using GameManager.WebApp.BS.Contracts;
using GameManager.WebApp.BS.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace GameManager.WebApp.BS.Repository
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await FindAll(false).Include(x => x.UserCategory).ToListAsync();
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            return await FindByCondition(x => x.RoleName == roleName, false).SingleOrDefaultAsync();
        }

        public async Task<List<Role>> GetRolesByCategory(int userCategoryId)
        {
            return await FindByCondition(x => x.UserCategoryId == userCategoryId, false).ToListAsync();
        }
    }
}
