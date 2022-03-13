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

        public IActionResult AddCard()
        {
            return View();
        }

        public IActionResult AddBucket()
        {
            return View();
        }
    }
}
