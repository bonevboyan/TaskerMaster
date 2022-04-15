namespace Taskord.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using Taskord.Data.Models;
    using Taskord.Services.Chats;

    [Authorize]
    public class ChatsController : Controller
    {
        private readonly IChatService chatService;
        private readonly UserManager<User> userManager;

        public ChatsController(IChatService chatService, UserManager<User> userManager)
        {
            this.chatService = chatService;
            this.userManager = userManager;
        }

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
    }
}
