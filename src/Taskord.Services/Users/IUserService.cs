namespace Taskord.Services.Users
{
    using Taskord.Services.Users.Models;

    public interface IUserService
    {
        IEnumerable<UserListServiceModel> GetTeamMembersList(string teamId, string userId);

        IEnumerable<UserListServiceModel> GetUserFriendsList(string userId);

        UserQueryServiceModel GetQueryUsers(string userId, string searchTerm, int currentPage, int usersPerPage);

        IEnumerable<UserListServiceModel> GetUserFriendRequests(string userId);

        IEnumerable<UserListServiceModel> GetUserPendingRequests(string userId);
    }
}
