namespace Taskord.Web.Components
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Data.Models;
    using Taskord.Services.Chats;
    using Taskord.Services.Teams;
    using Taskord.Web.Models;

    [ViewComponent(Name = "Teams")]
    public class TeamsViewComponent : ViewComponent
    {
        private readonly ITeamService teamService;
        private readonly IChatService chatService;
        private readonly UserManager<User> userManager;

        public TeamsViewComponent(ITeamService teamService, IChatService chatService, UserManager<User> userManager)
        {
            this.teamService = teamService;
            this.chatService = chatService;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var userId = this.userManager.GetUserId(Request.HttpContext.User);
            var teams = teamService.GetTeamList(userId);

            return this.View(teams);
        }
    }
}
