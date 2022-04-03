namespace Taskord.Web.Controllers.Api
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Data.Models;
    using Taskord.Services.Teams;
    using Taskord.Web.Models.Api;

    //[Route("api/teams/[action]")]
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

        [Authorize]
        [HttpPost]
        [Route("api/teams/inviteMember")]
        public IActionResult InviteMember(InviteMemberPostModel invitation)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!this.teamService.IsAdmin(userId, invitation.TeamId))
            {
                return this.Unauthorized();
            }

            try
            {
                this.teamService.SendTeamInvite(invitation.TeamId, userId, invitation.UserId);
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex);
            }


            return this.Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("api/teams/respondToInvite")]
        public IActionResult RespondToInvite(RespondToInvitePostModel response)
        {
            try
            {
                this.teamService.RespondToTeamInvite(response.TeamInviteId, response.IsAccepted);
                return this.Ok();
            }
            catch(ArgumentException)
            {
                return this.NotFound();
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/teams/withdrawInvite")]
        public IActionResult WithdrawInvite(WithdrawInvitePostModel withdraw)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!this.teamService.IsAdmin(userId, withdraw.TeamId))
            {
                return this.Unauthorized();
            }

            try
            {
                this.teamService.WithdrawTeamInvite(withdraw.TeamId, withdraw.UserId);
                return this.Ok();
            }
            catch (ArgumentException)
            {
                return this.NotFound();
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/teams/teamInvitesCount")]
        public string GetTeamInvitesCount()
        {
            var userId = this.userManager.GetUserId(this.User);

            var count = this.teamService.GetTeamInvites(userId).Count();

            return count.ToString();
        }

        [Authorize]
        [HttpPost]
        [Route("api/teams/manageChatUser")]
        public IActionResult ManageChatUser(ManageChatUserPostModel chatUser)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!this.teamService.IsAdmin(userId, chatUser.TeamId))
            {
                return this.Unauthorized();
            }

            try
            {
                this.teamService.ManageChatUser(chatUser.UserId, chatUser.ChatId, chatUser.TeamId);
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex);
            }


            return this.Ok();
        }
    }
}