namespace Taskord.Web.Models
{
    using System.Collections.Generic;
    using Taskord.Services.Teams.Models;
    using Taskord.Services.Users.Models;

    public class InviteMembersViewModel
    {
        public TeamServiceModel Team { get; set; }

        public IEnumerable<UserInviteListServiceModel> Friends { get; set; }
    }
}
