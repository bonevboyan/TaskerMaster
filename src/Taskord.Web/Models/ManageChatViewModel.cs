namespace Taskord.Web.Models
{
    using System.Collections.Generic;
    using Taskord.Services.Teams.Models;
    using Taskord.Services.Users.Models;

    public class ManageChatViewModel
    {
        public IEnumerable<UserTeamChatManageListServiceModel> Users { get; set; }

        public TeamServiceModel Team { get; set; }

        public string ChatName { get; set; }

        public string ChatId { get; set; }
    }
}
