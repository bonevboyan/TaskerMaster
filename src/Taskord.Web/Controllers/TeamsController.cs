using Microsoft.AspNetCore.Mvc;
using Taskord.Services.Teams;
using Taskord.Web.Models;

namespace Taskord.Web.Controllers
{
    public class TeamsController : Controller
    {
        private ITeamService teamService;

        public TeamsController(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateTeamFormModel teamFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View(teamFormModel);
            }

            teamService.Create(teamFormModel.Name, teamFormModel.Description, teamFormModel.ImagePath);

            return Redirect("/Home");
        }
    }
}
