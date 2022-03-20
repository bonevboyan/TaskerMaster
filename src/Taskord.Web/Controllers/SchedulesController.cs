namespace Taskord.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Web.Models;

    public class SchedulesController : Controller
    {
        [Authorize]
        public IActionResult Calendar()
        {
            return View();
        }

        [Authorize]
        public IActionResult Board()
        {
            return View();
        }

        [Authorize]
        public IActionResult AddCard()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddCard(CreateCardFormModel card)
        {
            return View();
        }

        [Authorize]
        public IActionResult AddBucket()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddBucket(CreateBucketFormModel bucket)
        {
            return View();
        }

        [Authorize]
        public IActionResult AddTag()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddTag(CreateTagFormModel tag)
        {
            return View();
        }
    }
}
