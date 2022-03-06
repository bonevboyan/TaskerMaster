namespace Taskord.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ChatsController : Controller
    {
        public IActionResult Personal(string chat)
        {
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
