namespace Taskord.Web.Models
{
    using System.Collections.Generic;
    using Taskord.Services.Chats.Models;

    public class ChatListViewModel
    {
        public string TeamId { get; set; }

        public bool IsAdmin { get; set; }

        public IEnumerable<ChatListServiceModel> Chats { get; set; }
    }
}
