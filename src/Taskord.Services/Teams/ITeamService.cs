namespace Taskord.Services.Teams
{
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Teams.Models;

    public interface ITeamService
    {
        string Create(string name, string description, string imageUrl, string userId);

        IEnumerable<TeamServiceModel> GetTeamList(string userId);

        TeamServiceModel GetTeam(string teamId);

        bool IsAdmin(string userId, string teamId);

        string SendTeamInvite(string teamId, string senderId, string receiverId);

        IEnumerable<TeamInviteServiceModel> GetTeamInvites(string userId);

        string RespondToTeamInvite(string teamInviteId, bool isAccepted);

        string WithdrawTeamInvite(string teamId, string userId);

        void ManageChatUser(string userId, string chatId, string teamId);

        RelationshipState? IsUserInvited(string userId, string teamId);
    }
}
