namespace Taskord.Web.Controllers.Api
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Data.Models;
    using Taskord.Services.Chats;
    using Taskord.Web.Models.Api;

    [Route("api/chats")]
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

        [Authorize]
        [HttpPost]
        public IActionResult InviteMember(MessagePostModel message)
        {
            var userId = this.userManager.GetUserId(this.User);

            try
            {
                this.chatService.SendMessage(message.ChatId, userId, message.Content);

                return this.Ok();
            }
            catch(ArgumentException ex)
            {
                return this.BadRequest(ex);
            }

        }
    }
}
