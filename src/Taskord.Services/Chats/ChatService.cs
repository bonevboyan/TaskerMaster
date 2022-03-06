namespace Taskord.Services.Chats
{
    using System.Collections.Generic;
    using System.Linq;
    using Taskord.Data;
    using Taskord.Services.Chats.Models;

    public class ChatService : IChatService
    {

        private readonly TaskordDbContext data;

        public ChatService(TaskordDbContext data)
        {
            this.data = data;
        }

        public ChatServiceModel GetChat(string chatId)
        {
            var chat = data.Chats
                .Find(chatId);

            ChatServiceModel chatServiceModel = new ChatServiceModel
            {
                Name = chat.Name,
                Members = chat.Users
                    .Select(u => new ChatMemberServiceModel
                    {
                        Username = u.UserName,
                        ImagePath = u.ImagePath
                    }),
                Messages = chat.Messages
                    .Select(m => new ChatMessageServiceModel
                    {
                        Content = m.Content,
                        Sender = new ChatMemberServiceModel
                        {
                            Username = m.ApplicationUser.UserName,
                            ImagePath = m.ApplicationUser.ImagePath
                        }
                    })
            };

            return chatServiceModel;
        }

        public IEnumerable<ChatListServiceModel> GetChatNames(string userId)
        {
            var chats = data.Chats
                .Select(x => new ChatListServiceModel
                {
                    Name = x.Name
                })
                .ToList();

            return chats;
        }
    }
}
