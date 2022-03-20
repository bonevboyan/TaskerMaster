namespace Taskord.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Data.Models;
    using Taskord.Services.Chats;
    using Taskord.Services.Users;
    using Taskord.Web.Models;

    public class ChatsController : Controller
    {
        private readonly IChatService chatService;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public ChatsController(IChatService chatService, IUserService userService, UserManager<ApplicationUser> userManager)
        {
            this.chatService = chatService;
            this.userService = userService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Personal(string chat)
        {
            return View();
        }

        [Authorize]
        public IActionResult ById(string team, string chat)
        {
            if(team == "@me")
            {

            }

            return View();
        }

        [Authorize]
        public IActionResult Create(string teamId)
        {
            var users = userService.GetTeamMembersList(teamId);

            return this.View(new CreateChatFormModel
            {
                UserIds = users
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(string teamId, CreateChatFormModel chat)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(chat);
            }
            var userId = this.userManager.GetUserId(this.User);
            var users = chat == null ? chat.UserIds.Select(x => x.Id).ToList() : new List<string>();
            users.Add(userId);

            string newChatId = chatService.CreateChat(teamId, chat.Name, users);

            return this.Redirect($"chats/{teamId}/{newChatId}");
        }
    }
}
