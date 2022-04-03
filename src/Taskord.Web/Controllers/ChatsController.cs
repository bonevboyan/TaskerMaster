namespace Taskord.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using Taskord.Data.Models;
    using Taskord.Services.Chats;
    using Taskord.Services.Users;
    using Taskord.Web.Models;

    public class ChatsController : Controller
    {
        private readonly IChatService chatService;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public ChatsController(IChatService chatService, IUserService userService, UserManager<User> userManager)
        {
            this.chatService = chatService;
            this.userService = userService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Get(string teamId, string chatId)
        {
            var userId = this.userManager.GetUserId(this.User);

            try
            {
                if(teamId == "me")
                {
                    var chat = this.chatService.GetPersonalChat(userId, chatId);

                    return this.View("Chats", chat);
                }
                else
                {
                    var chat = this.chatService.GetTeamChat(userId, teamId, chatId);

                    return this.View("Chats", chat);
                }
            }
            catch (ArgumentException ex)
            {
                return this.StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //[Authorize]
        //public IActionResult Create(string teamId)
        //{
        //    var userId = this.userManager.GetUserId(this.User);

        //    var users = this.userService.GetTeamMembersList(teamId, userId);

        //    return this.View(new CreateChatFormModel
        //    {
        //        UserIds = users
        //    });
        //}

        //[Authorize]
        //[HttpPost]
        //public IActionResult Create(string teamId, CreateChatFormModel chat)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(chat);
        //    }
        //    var userId = this.userManager.GetUserId(this.User);
        //    var users = chat == null ? chat.UserIds.Select(x => x.Id).ToList() : new List<string>();
        //    users.Add(userId);

        //    try
        //    {
        //        string newChatId = this.chatService.CreateChat(teamId, chat.Name, users);

        //        return this.Redirect($"chats/{teamId}/{newChatId}");
        //    }
        //    catch(ArgumentException ex)
        //    {
        //        return this.BadRequest(ex);
        //    }

        //}
    }
}
