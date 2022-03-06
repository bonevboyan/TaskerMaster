using Microsoft.AspNetCore.Mvc;
using Taskord.Web.Models;

namespace Taskord.Web.Controllers
{
    public class TeamsController : Controller
    {

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

            return Redirect("/Home");
        }
    }
}
