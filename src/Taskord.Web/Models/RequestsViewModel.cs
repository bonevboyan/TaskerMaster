namespace Taskord.Web.Models
{
    using Taskord.Services.Teams.Models;
    using Taskord.Services.Users.Models;

    public class RequestsViewModel
    {
        public IEnumerable<UserListServiceModel> ReceivedRequests { get; set; }

        public IEnumerable<UserListServiceModel> SentRequests { get; set; }

        public IEnumerable <TeamInviteServiceModel> TeamInvites { get; set; }
    }
}
