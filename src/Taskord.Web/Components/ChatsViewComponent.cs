namespace Taskord.Web.Components
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Taskord.Data.Models;
    using Taskord.Services.Chats;
    using Taskord.Services.Teams;
    using Taskord.Web.Models;

    [ViewComponent(Name = "Chats")]
    public class ChatsViewComponent : ViewComponent
    {
        private readonly IChatService chatService;
        private readonly ITeamService teamService;
        private readonly UserManager<User> userManager;

        public ChatsViewComponent(IChatService chatService, UserManager<User> userManager, ITeamService teamService)
        {
            this.chatService = chatService;
            this.userManager = userManager;
            this.teamService = teamService;
        }

        public IViewComponentResult Invoke(string teamId, string chatId)
        {
            var userId = this.userManager.GetUserId(this.Request.HttpContext.User);

            var chats = this.chatService.GetChatList(teamId, userId, chatId);

            return this.View(new ChatListViewModel
            {
                Chats = chats,
                TeamId = teamId,
                IsAdmin = this.teamService.IsAdmin(userId, teamId)
            });
        }
    }
}
