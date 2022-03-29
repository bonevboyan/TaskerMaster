namespace Taskord.Services.Chats
{
    using Microsoft.EntityFrameworkCore;
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
            var chat = this.data.Chats
                .Find(chatId);

            return new ChatServiceModel
            {
                Id = chat.Id,
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
                        DateTime = m.CreatedOn.ToString("MM/dd HH:mm"),
                        Sender = new ChatMemberServiceModel
                        {
                            Username = m.User.UserName,
                            ImagePath = m.User.ImagePath
                        }
                    })
            }; ;
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

            var chatName = "Personal Chat";

            var chat = new Chat
            {
                ChatType = ChatType.Personal,
                Users = users,
                Name = chatName
            };

            var chatUser = new ChatUser
            {
                ChatId = chat.Id,
                UserId = firstUserId,
                IsRead = false
            };

            var secondChatUser = new ChatUser
            {
                ChatId = chat.Id,
                UserId = secondUserId,
                IsRead = false
            };

            this.data.Chats.Add(chat);
            this.data.ChatUsers.Add(chatUser);
            this.data.ChatUsers.Add(secondChatUser);

            this.data.SaveChanges();

            return chat.Id;
        }

        public ChatServiceModel GetPersonalChat(string userId, string secondUserId)
        {
            var chat = new Chat();

            var friend = new User();

            if (secondUserId is null)
            {
                chat = this.data.Chats
                    .Include(x => x.Users)
                    .ThenInclude(x => x.Messages)
                    .OrderByDescending(x => x.Messages.OrderByDescending(m => m.CreatedOn).First())
                    .FirstOrDefault(x => x.Users.Any(u => u.Id == userId)
                        && x.ChatType == ChatType.Personal);

                if (chat is null)
                {
                    return null;
                }
            }
            else
            {
                chat = this.data.Chats
                    .Include(x => x.Users)
                    .FirstOrDefault(x => x.Users.Any(u => u.Id == userId)
                        && x.Users.Any(u => u.Id == secondUserId)
                        && x.ChatType == ChatType.Personal);

                if (chat is null)
                {
                    throw new ArgumentException(InvalidChatParticipants);
                }
                
            }

            friend = chat.Users.FirstOrDefault(x => x.Id != userId);

            var chatUser = this.data.ChatUsers.FirstOrDefault(x => x.UserId == userId && x.ChatId == chat.Id);
            chatUser.IsRead = true;
            this.data.SaveChanges();

            var messages = this.data.Messages
                .Include(x => x.User)
                .Where(x => x.ChatId == chat.Id)
                .ToList();

            var chatModel = new ChatServiceModel
            {
                Id = chat.Id,
                Messages = messages
                    .OrderBy(x => x.CreatedOn)
                    .Select(x => new ChatMessageServiceModel
                    {
                        Content = x.Content,
                        DateTime = x.CreatedOn.ToString("MM/dd HH:mm"),
                        IsOwn = x.UserId == userId,
                        Sender = new ChatMemberServiceModel
                        {
                            ImagePath = x.User.ImagePath,
                            Username = x.User.UserName
                        }
                    }).ToList(),
                Members = chat.Users.Select(x => new ChatMemberServiceModel
                {
                    ImagePath = x.ImagePath,
                    Username = x.UserName
                }).ToList(),
                Name = friend.UserName
            };

            return chatModel;
        }

        public string SendMessage(string chatId, string userId, string content)
        {   
            var chat = this.data.Chats
                .Include(x => x.Users)
                .FirstOrDefault(x => x.Id == chatId);

            if(chat is null)
            {
                throw new ArgumentException(InvalidChat);
            }

            if(!chat.Users.Any(x => x.Id == userId))
            {
                throw new ArgumentException(UserNotInChat);
            }

            var message = new Message
            {
                UserId = userId,
                Content = content,
                ChatId = chatId
            };

            var chatUsers = this.data.ChatUsers.Where(x => x.UserId != userId && x.ChatId == chat.Id).ToList();
            foreach (var chatUser in chatUsers)
            {
                chatUser.IsRead = false;
            }

            this.data.Messages.Add(message);

            this.data.SaveChanges();

            var isRead = this.data.ChatUsers.FirstOrDefault(x => x.UserId == userId && x.ChatId == chat.Id).IsRead;

            return message.Id;
        }
    }
}
