namespace Taskord.Services.Chats.Models
{
    public class ChatListServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ChatMessageServiceModel LastMessageSent { get; set; }

        public bool IsRead { get; set; }

        public bool IsSelected { get; set; }
    }
}
