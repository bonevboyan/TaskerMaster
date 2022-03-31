namespace Taskord.Web.Components
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Data.Models;
    using Taskord.Services.Users;

    [ViewComponent(Name = "Friends")]
    public class FriendsViewComponent : ViewComponent
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public FriendsViewComponent(IUserService userService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke(string chatId)
        {
            var userId = this.userManager.GetUserId(this.Request.HttpContext.User);

            var friends = this.userService.GetUserFriendsList(userId, chatId);

            return this.View(friends);
        }
    }
}
