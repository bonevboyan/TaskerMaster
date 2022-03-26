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
            var userId = this.userManager.GetUserId(this.User);

            var users = this.userService.GetTeamMembersList(this.userManager.GetUserId(this.User), userId);

            return this.View(new CreateChatFormModel
            {
                UserIds = users
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateTeamFormModel team)
        {
            if (!ModelState.IsValid)
            {
                return View(team);
            }

            var users = team == null ? team.UserIds.Select(x => x.Id).ToList() : new List<string>();
            users.Add(this.userManager.GetUserId(this.User));

            this.teamService.Create(team.Name, team.Description, team.ImagePath, users);

            return this.Redirect("/Home");
        }
    }
}
