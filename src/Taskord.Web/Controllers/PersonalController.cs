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
        private readonly UserManager<User> userManager;

        public PersonalController(IUserService userService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult All(string userId)
        {
            var myUserId = this.userManager.GetUserId(this.User);

            var friends = this.userService.GetUserFriendsList(myUserId);

            return this.View(friends);
        }

        [Authorize]
        public IActionResult SendRequest(string userId)
        {
            // TODO

            return this.Redirect("/all");
        }

        [Authorize]
        public IActionResult Chats(string userId)
        {
            // TODO

            return this.View();
        }

        [Authorize]
        public IActionResult Search([FromQuery] UserQueryModel query)
        {
            var userId = this.userManager.GetUserId(this.User);

            var queryResult = this.userService.GetQueryUsers(
                userId,
                query.SearchTerm,
                query.CurrentPage,
                UserQueryModel.UsersPerPage);

            query.TotalUsers = queryResult.TotalUsers;
            query.Users = queryResult.Users;

            return this.View(query);
        }
    }
}
