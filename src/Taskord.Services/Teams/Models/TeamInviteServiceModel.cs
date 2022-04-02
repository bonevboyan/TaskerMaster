namespace Taskord.Services.Teams.Models
{
    using Taskord.Services.Users.Models;

    public class TeamInviteServiceModel
    {
        public string Id { get; set; }

        public TeamListServiceModel Team { get; set; }

        public UserListServiceModel Sender { get; set; }
    }
}
