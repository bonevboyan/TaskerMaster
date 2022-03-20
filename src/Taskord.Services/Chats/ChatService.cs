namespace Taskord.Services.Chats
{
    using System.Collections.Generic;
    using System.Linq;
    using Taskord.Data;
    using Taskord.Data.Models;
    using Taskord.Services.Chats.Models;

    public class ChatService : IChatService
    {

        private readonly TaskordDbContext data;

        public ChatService(TaskordDbContext data)
        {
            this.data = data;
        }

        public string CreateChat(string teamId, string name, IEnumerable<string> userIds)
        {
            var team = data.Teams.FirstOrDefault(x => x.Id == teamId);

            if (team == null)
            {
                throw new ArgumentException("Team not found!");
            }

            var matchedUsers = data.ApplicationUsers.Where(u => userIds.Any(x => x == u.Id)).ToList();

            if (matchedUsers.Count() != userIds.Count())
            {
                throw new ArgumentException("User Ids not found!");
            }

            Chat chat = new Chat
            {
                Name = name,
                Users = matchedUsers
            };

            team.Chats.Add(chat);
            data.SaveChanges();

            return chat.Id;
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

        public IEnumerable<ChatListServiceModel> GetChatList(string teamId)
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
