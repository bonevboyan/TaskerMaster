namespace Taskord.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class SchedulesController : Controller
    {
        public IActionResult Calendar()
        {
            return View();
        }

        public IActionResult Board()
        {
            return View();
        }
    }
}
