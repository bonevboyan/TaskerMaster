namespace Taskord.Web.Models
{
    using Taskord.Services.Chats.Models;

    public class ChatListViewModel
    {
        public string TeamId { get; set; }

        public bool IsAdmin { get; set; }

        public IEnumerable<ChatListServiceModel> Chats { get; set; }
    }
}
