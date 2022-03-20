namespace Taskord.Web.Components
{
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Services.Chats;
    using Taskord.Services.Teams;
    using Taskord.Web.Models;

    [ViewComponent(Name = "Sidebar")]
    public class SidebarViewComponent : ViewComponent
    {
        private readonly ITeamService teamService;
        private readonly IChatService chatService;

        public SidebarViewComponent(ITeamService teamService, IChatService chatService)
        {
            this.teamService = teamService;
            this.chatService = chatService;
        }

        public IViewComponentResult Invoke(string userId, string teamId)
        {
            var teams = teamService.GetTeamList(userId);
            var chatNames = chatService.GetChatList(teamId);

            var sidebar = new SidebarViewModel
            {
                Teams = teams,
                Chats = chatNames
            };

            return this.View(sidebar);
        }
    }
}
