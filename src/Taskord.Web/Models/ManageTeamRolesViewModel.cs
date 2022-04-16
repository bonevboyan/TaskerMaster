namespace Taskord.Web.Models
{
    using System.Collections.Generic;
    using Taskord.Services.Teams.Models;
    using Taskord.Services.Users.Models;

    public class ManageTeamRolesViewModel
    {
        public IEnumerable<UserManageRolesServiceModel> Users { get; set; }

        public TeamServiceModel Team { get; set; }
    }
}
