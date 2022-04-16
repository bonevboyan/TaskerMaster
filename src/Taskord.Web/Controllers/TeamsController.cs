using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Taskord.Data.Models;
using Taskord.Services.Chats;
using Taskord.Services.Teams;
using Taskord.Services.Users;
using Taskord.Web.Models;

namespace Taskord.Web.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamService teamService;
        private readonly IUserService userService;
        private readonly IChatService chatService;
        private readonly UserManager<User> userManager;

        public TeamsController(ITeamService teamService, IUserService userService, UserManager<User> userManager, IChatService chatService)
        {
            this.teamService = teamService;
            this.userService = userService;
            this.userManager = userManager;
            this.chatService = chatService;
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

            return this.Redirect($"/Teams/{teamId}/InviteMembers");
        }

        [Authorize]
        public IActionResult InviteMembers(string teamId)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!this.teamService.IsAdmin(userId, teamId))
            {
                return this.Unauthorized();
            }

            var team = this.teamService.GetTeam(teamId);
            var friends = this.userService.GetInviteFriendsList(userId, teamId);

            return this.View(new InviteMembersViewModel
            {
                Friends = friends,
                Team = team
            });
        }

        [Authorize]
        public IActionResult ManageChatMembers(string teamId, string chatId)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!this.teamService.IsAdmin(userId, teamId))
            {
                return this.Unauthorized();
            }

            var team = this.teamService.GetTeam(teamId);
            var teamMembers = this.userService.GetTeamChatMembersList(userId, teamId, chatId);
            var chat = this.chatService.GetTeamChat(userId, teamId, chatId);
            
            return this.View(new ManageChatViewModel
            {
                Users = teamMembers,
                Team = team,
                ChatName = chat.Name,
                ChatId = chat.Id
            });
        }

        [Authorize]
        public IActionResult ManageMemberRoles(string teamId)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (!this.teamService.IsAdmin(userId, teamId))
            {
                return this.Unauthorized();
            }

            var team = this.teamService.GetTeam(teamId);
            var teamMembers = this.userService.GetRoleManageTeamMembersList(userId, teamId);

            return this.View(new ManageTeamRolesViewModel
            {
                Team = team,
                Users = teamMembers
            });
        }


    }
}
