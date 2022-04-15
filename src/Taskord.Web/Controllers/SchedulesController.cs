namespace Taskord.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Services.Schedules;
    using Taskord.Web.Models;

    public class SchedulesController : Controller
    {
        private readonly IScheduleService scheduleService;

        public SchedulesController(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        [Authorize]
        public IActionResult Calendar()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Board(string teamId)
        {
            var board = this.scheduleService.GetBoard(teamId);
            return this.View(board);
        }

        [Authorize]
        public IActionResult AddCard()
        {
            return this.View();
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
