namespace Taskord.Web.Controllers.Api
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Data.Models;
    using Taskord.Services.Chats;
    using Taskord.Services.Teams;
    using Taskord.Web.Models.Api;

    [Authorize]
    [ApiController]
    public class ChatsApiController : ControllerBase
    {
        private readonly IChatService chatService;
        private readonly ITeamService teamService;
        private readonly UserManager<User> userManager;

        public ChatsApiController(IChatService chatService, UserManager<User> userManager, ITeamService teamService)
        {
            this.chatService = chatService;
            this.userManager = userManager;
            this.teamService = teamService;
        }

        [HttpPost]
        [Route("api/chats/sendMessage")]
        public IActionResult SendMessage(MessagePostModel message)
        {
            var userId = this.userManager.GetUserId(this.User);

            try
            {
                this.chatService.SendMessage(message.ChatId, userId, message.Content);

                return this.Ok();
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex);
            }

        }

        [HttpPost]
        [Route("api/chats/addChat")]
        public IActionResult AddChat(AddChatPostModel chat)
        {
            var userId = this.userManager.GetUserId(this.User);

            if(!this.teamService.IsAdmin(userId, chat.TeamId))
            {
                return this.Unauthorized();
            }

            try
            {
                this.chatService.CreateTeamChat(chat.TeamId, chat.Name);

                return this.Ok();
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex);
            }

        }
    }
}
