namespace Taskord.Services.Chats.Models
{
    public class ChatServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string LastReadMessageId { get; set; }

        public bool IsPersonal { get; set; }

        public string TeamId { get; set; }

        public IEnumerable<ChatMemberServiceModel> Members { get; set; }

        public IEnumerable<ChatMessageServiceModel> Messages { get; set; }
    }
}
