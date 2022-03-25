namespace Taskord.Services.Chats
{
    using System.Collections.Generic;
    using System.Linq;
    using Taskord.Data;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Chats.Models;

    using static Taskord.Common.ErrorMessages.Chat;

    public class ChatService : IChatService
    {

        private readonly TaskordDbContext data;

        public ChatService(TaskordDbContext data)
        {
            this.data = data;
        }

        public string CreateChat(string teamId, string name, IEnumerable<string> userIds)
        {
            var team = this.data.Teams.FirstOrDefault(x => x.Id == teamId);

            if (team == null)
            {
                throw new ArgumentException(InvalidTeam);
            }

            var matchedUsers = this.data.Users.Where(u => userIds.Any(x => x == u.Id)).ToList();

            if (matchedUsers.Count != userIds.Count())
            {
                throw new ArgumentException(InvalidUsers);
            }

            var chat = new Chat
            {
                Name = name,
                Users = matchedUsers,
                ChatType = ChatType.Team
            };

            team.Chats.Add(chat);
            this.data.SaveChanges();

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
                            Username = m.User.UserName,
                            ImagePath = m.User.ImagePath
                        }
                    })
            };

            return chatServiceModel;
        }

        public IEnumerable<ChatListServiceModel> GetChatList(string teamId)
        {
            var chats = this.data.Chats
                .Select(x => new ChatListServiceModel
                {
                    Name = x.Name
                })
                .ToList();

            return chats;
        }

        public string CreatePersonalChat(string firstUserId, string secondUserId)
        {
            var users = this.data.Users.Where(x => x.Id == firstUserId || x.Id == secondUserId).ToList();

            if (users.Count != 2)
            {
                throw new ArgumentException(InvalidUsers);
            }

            var chatName = string.Format($"{users[0].UserName} and {users[1].UserName}'s chat");

            var chat = new Chat
            {
                ChatType = ChatType.Personal,
                Users = users,
                Name = chatName
            };

            this.data.Chats.Add(chat);

            this.data.SaveChanges();

            return chat.Id;
        }

        public ChatServiceModel GetPersonalChat(string firstUserId, string secondUserId)
        {
            var chat = this.data.Chats
                .FirstOrDefault(x => x.Users.Any(u => u.Id == firstUserId) 
                    && x.Users.Any(u => u.Id == secondUserId) 
                    && x.ChatType == ChatType.Personal);

            if(chat is null)
            {
                throw new ArgumentException(InvalidChatParticipants);
            }

            return new ChatServiceModel
            {
                Messages = chat.Messages.Select(x => new ChatMessageServiceModel
                {
                    Content = x.Content,
                    Sender = new ChatMemberServiceModel
                    {
                        ImagePath = x.User.ImagePath,
                        Username = x.User.UserName
                    }
                }),
                Members = chat.Users.Select(x => new ChatMemberServiceModel
                {
                    ImagePath = x.ImagePath,
                    Username = x.UserName
                }),
                Name = chat.Name
            };
        }
    }
}
