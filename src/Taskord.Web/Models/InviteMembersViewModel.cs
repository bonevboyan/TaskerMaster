namespace Taskord.Web.Models
{
    using Taskord.Services.Teams.Models;
    using Taskord.Services.Users.Models;

    public class InviteMembersViewModel
    {
        public TeamListServiceModel Team { get; set; }

        public IEnumerable<UserListServiceModel> Friends { get; set; }
    }
}
