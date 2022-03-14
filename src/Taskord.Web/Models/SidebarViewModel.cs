namespace Taskord.Web.Models
{
    using Taskord.Services.Chats.Models;
    using Taskord.Services.Teams.Models;

    public class SidebarViewModel
    {
        public IEnumerable<TeamListServiceModel> Teams { get; set; }

        public IEnumerable<ChatListServiceModel> Chats { get; set; }
    }
}
