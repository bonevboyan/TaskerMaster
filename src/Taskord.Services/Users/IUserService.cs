namespace Taskord.Services.Users
{
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Users.Models;

    public interface IUserService
    {
        IEnumerable<UserListServiceModel> GetTeamMembersList(string teamId, string userId);

        IEnumerable<FriendsChatListServiceModel> GetFriendsChatList(string userId, string chatId = null);

        IEnumerable<UserInviteListServiceModel> GetInviteFriendsList(string userId);

        UserQueryServiceModel GetQueryUsers(string userId, string searchTerm, int currentPage, int usersPerPage);

        IEnumerable<UserListServiceModel> GetUserReceivedFriendRequests(string userId);

        IEnumerable<UserListServiceModel> GetUserSentFriendRequests(string userId);

        string SendFriendRequest(string senderId, string receiverId);

        void ChangeRelationshipState(string senderId, string receiverId, RelationshipState isAccepted);
    }
}
