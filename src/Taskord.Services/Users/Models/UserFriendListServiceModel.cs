namespace Taskord.Services.Users.Models
{
    using Taskord.Services.Chats.Models;

    public class FriendsChatListServiceModel : UserListServiceModel
    {
        public ChatMessageServiceModel LastMessageSent { get; set; }

        public bool IsRead { get; set; }

        public bool IsSelected { get; set; }
    }
}
