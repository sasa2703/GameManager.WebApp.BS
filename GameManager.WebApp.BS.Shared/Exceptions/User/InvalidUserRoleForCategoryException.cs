namespace GameManager.WebApp.BS.Shared.Exceptions.User
{
    public sealed class InvalidUserRoleForCategoryException : BadRequestException
    {
        public InvalidUserRoleForCategoryException(int roleId, int userCategoryId) : base($"Role with roleID {roleId} cannot be assigned to user with categoryId {userCategoryId}")
        {
        }
    }
}
