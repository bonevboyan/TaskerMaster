namespace Taskord.Services.Chats.Models
{
    using Taskord.Data.Models;

    public class ChatServiceModel
    {
        public string Name { get; set; }

        public IEnumerable<ChatMemberServiceModel> Members { get; set; }

        public IEnumerable<ChatMessageServiceModel> Messages { get; set; }
    }
}
