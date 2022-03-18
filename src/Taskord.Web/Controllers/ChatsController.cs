namespace Taskord.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Services.Chats;
    using Taskord.Services.Users;

    public class ChatsController : Controller
    {
        private readonly IChatService chatService;
        private readonly IUserService userService;

        public ChatsController(IChatService chatService)
        {
            this.chatService = chatService;
        }

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

        public IActionResult Create(string teamId)
        {
            var users = userService.GetUserList(teamId);

            return this.View(users);
        }
    }
}
