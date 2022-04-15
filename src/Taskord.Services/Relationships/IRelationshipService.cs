namespace Taskord.Services.Relationships
{
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Users.Models;

    public interface IRelationshipService
    {
        IEnumerable<UserListServiceModel> GetUserReceivedFriendRequests(string userId);

        IEnumerable<UserListServiceModel> GetUserSentFriendRequests(string userId);

        string SendFriendRequest(string senderId, string receiverId);

        void ChangeRelationshipState(string senderId, string receiverId, RelationshipState isAccepted);

        RelationshipState? GetRelationshipState(string userId, string secondUserId);
    }
}
