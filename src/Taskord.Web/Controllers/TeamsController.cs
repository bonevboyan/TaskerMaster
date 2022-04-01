using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Taskord.Data.Models;
using Taskord.Services.Teams;
using Taskord.Services.Users;
using Taskord.Web.Models;

namespace Taskord.Web.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamService teamService;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public TeamsController(ITeamService teamService, IUserService userService, UserManager<User> userManager)
        {
            this.teamService = teamService;
            this.userService = userService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View(new CreateTeamFormModel());
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateTeamFormModel team)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(team);
            }

            var userId = this.userManager.GetUserId(this.User);

            var teamId = this.teamService.Create(team.Name, team.Description, team.ImagePath, userId);

            return this.Redirect($"Teams/{teamId}/InviteMembers");
        }

        [Authorize]
        public IActionResult InviteMembers(string teamId)
        {
            var userId = this.userManager.GetUserId(this.User);

            if(!this.teamService.IsAdmin(userId, teamId))
            {
                return this.Unauthorized();
            }

            var team = this.teamService.GetTeam(teamId);
            var friends = this.userService.GetInviteFriendsList(userId);

            return this.View(new InviteMembersViewModel
            {
                Friends = friends,
                Team = team
            });
        }

        [Authorize]
        public IActionResult ManageMembers(string teamId)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!this.teamService.IsAdmin(userId, teamId))
            {
                return this.Unauthorized();
            }

            var team = this.teamService.GetTeam(teamId);

            return this.View(team);
        }
    }
}
