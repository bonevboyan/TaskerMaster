namespace Taskord.Services.Chats.Models
{
    public class ChatMessageServiceModel
    {
        public string Content { get; set; }

        public ChatMemberServiceModel Sender { get; set; }
    }
}
