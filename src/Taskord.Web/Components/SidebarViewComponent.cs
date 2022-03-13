namespace Taskord.Web.Components
{
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Services.Teams;

    [ViewComponent(Name = "Sidebar")]
    public class SidebarViewComponent : ViewComponent
    {
        private readonly ITeamService teamService;

        public SidebarViewComponent(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public IViewComponentResult Invoke(string userId)
        {
            var teams = teamService.GetTeamList(userId);

            return this.View(teams);
        }
    }
}
