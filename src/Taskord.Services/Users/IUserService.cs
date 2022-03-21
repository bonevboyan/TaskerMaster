namespace Taskord.Services.Users
{
    using Taskord.Services.Users.Models;

    public interface IUserService
    {
        IEnumerable<UserListServiceModel> GetTeamMembersList(string teamId);

        IEnumerable<UserListServiceModel> GetUserFriendsList(string userId);

        IEnumerable<UserListServiceModel> GetUsersBySearchTerm(string searchTerm);
    }
}
