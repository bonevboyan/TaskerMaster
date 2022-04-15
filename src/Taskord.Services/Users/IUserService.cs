namespace Taskord.Services.Users
{
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Users.Models;

    public interface IUserService
    {
        IEnumerable<FriendsChatListServiceModel> GetFriendsChatList(string userId, string chatId);

        IEnumerable<UserInviteListServiceModel> GetInviteFriendsList(string userId, string teamId);

        IEnumerable<UserTeamChatManageListServiceModel> GetTeamChatMembersList(string userId, string teamId, string chatId);

        IEnumerable<UserManageRolesServiceModel> GetRoleManageTeamMembersList(string userId, string teamId);

        UserQueryServiceModel GetQueryUsers(string userId, string searchTerm, int currentPage, int usersPerPage);

        IEnumerable<UserListServiceModel> GetUserReceivedFriendRequests(string userId);

        IEnumerable<UserListServiceModel> GetUserSentFriendRequests(string userId);

        string SendFriendRequest(string senderId, string receiverId);

        void ChangeRelationshipState(string senderId, string receiverId, RelationshipState isAccepted);

        RelationshipState? GetRelationshipState(string userId, string secondUserId);


    }
}
