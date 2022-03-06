namespace Taskord.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ChatsController : Controller
    {
        public IActionResult ById(string chat)
        {
            if (team == "@me")
            {

            }

            return View();
        }

        public IActionResult ById(string team, string chat)
        {
            if(team == "@me")
            {

            }

            return View();
        }
    }
}
