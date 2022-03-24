namespace Taskord.Services.Users
{
    using Taskord.Services.Users.Models;

    public interface IUserService
    {
        IEnumerable<UserListServiceModel> GetTeamMembersList(string teamId, string userId);

        IEnumerable<UserListServiceModel> GetUserFriendsList(string userId);

        UserQueryServiceModel GetQueryUsers(string userId, string searchTerm, int currentPage, int usersPerPage);

        IEnumerable<UserListServiceModel> GetUserReceivedFriendRequests(string userId);

        IEnumerable<UserListServiceModel> GetUserSentFriendRequests(string userId);

        string SendFriendRequest(string senderId, string receiverId);

        void RespondToFriendRequest(string senderId, string receiverId, bool isAccepted);
    }
}
