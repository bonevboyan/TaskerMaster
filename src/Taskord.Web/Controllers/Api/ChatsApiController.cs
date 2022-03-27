namespace Taskord.Web.Controllers.Api
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Data.Models;
    using Taskord.Services.Chats;
    using Taskord.Web.Models.Api;

    [Route("api/me")]
    [ApiController]
    public class ChatsApiController : ControllerBase
    {
        private readonly IChatService chatService;
        private readonly UserManager<User> userManager;

        public ChatsApiController(IChatService chatService, UserManager<User> userManager)
        {
            this.chatService = chatService;
            this.userManager = userManager;
        }

        [HttpPost]
        public void SendMessage(MessagePostModel message)
        {
            var userId = this.userManager.GetUserId(this.User);

            this.chatService.SendMessage(message.ChatId, userId, message.Content);
        }
    }
}
