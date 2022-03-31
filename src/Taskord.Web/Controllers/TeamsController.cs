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

            var users = this.userService.GetUserFriendsList(userId);

            var bools = new List<bool>(users.Count());

            foreach (var user in users)
            {
                bools.Add(false);
            }

            return this.View(new CreateTeamFormModel
            {
                UserIds = users,
                SelectedUserIds = bools
            });
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

            this.teamService.Create(team.Name, team.Description, team.ImagePath, userId);

            return this.Redirect("/Home");
        }
    }
}
