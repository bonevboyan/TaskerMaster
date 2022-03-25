namespace Taskord.Web.Models
{
    using Taskord.Services.Users.Models;

    public class FriendRequestsViewModel
    {
        public IEnumerable<UserListServiceModel> ReceivedRequests { get; set; }

        public IEnumerable<UserListServiceModel> SentRequests { get; set; }
    }
}
