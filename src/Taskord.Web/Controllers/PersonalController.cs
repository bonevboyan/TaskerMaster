namespace Taskord.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Data.Models;
    using Taskord.Services.Users;
    using Taskord.Web.Models;

    public class PersonalController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public PersonalController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult All()
        {
            var userId = this.userManager.GetUserId(this.User);

            var friends = this.userService.GetUserFriendsList(userId);

            return View(friends);
        }

        [Authorize]
        public IActionResult Search([FromQuery] UserQueryModel query)
        {
            // TODO

            return View();
        }
    }
}
