namespace Taskord.Web.Components
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Data.Models;
    using Taskord.Services.Teams;

    [ViewComponent(Name = "Teams")]
    public class TeamsViewComponent : ViewComponent
    {
        private readonly ITeamService teamService;
        private readonly UserManager<User> userManager;

        public TeamsViewComponent(ITeamService teamService, UserManager<User> userManager)
        {
            this.teamService = teamService;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var userId = this.userManager.GetUserId(this.Request.HttpContext.User);
            var teams = this.teamService.GetTeamList(userId);

            return this.View(teams);
        }
    }
}
