namespace Taskord.Services.Chats.Models
{
    using Taskord.Services.Users.Models;

    public class ChatMessageServiceModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string DateTime { get; set; }

        public bool IsOwn { get; set; }

        public UserListServiceModel Sender { get; set; }
    }
}
