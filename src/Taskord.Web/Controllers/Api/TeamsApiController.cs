namespace Taskord.Web.Controllers.Api
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Data.Models;
    using Taskord.Services.Teams;
    using Taskord.Web.Models.Api;

    [Route("api/teams")]
    [ApiController]
    public class TeamsApiController : ControllerBase
    {
        private readonly ITeamService teamService;
        private readonly UserManager<User> userManager;

        public TeamsApiController(ITeamService teamService, UserManager<User> userManager)
        {
            this.teamService = teamService;
            this.userManager = userManager;
        }

        [HttpPost]
        public IActionResult InviteMember(InviteMemberPostModel invitation)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!this.teamService.IsAdmin(userId, invitation.TeamId))
            {
                return this.Unauthorized();
            }

            this.teamService.SendTeamInvite(invitation.TeamId, userId, invitation.UserId);

            return this.Ok();
        }
    }
}
