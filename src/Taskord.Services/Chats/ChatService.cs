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

        public ChatServiceModel GetTeamChat(string userId, string teamId, string chatId)
        {
            var chats = this.data.Chats
                    .Include(x => x.Messages)
                    .Include(x => x.Users);

            var chat = new Chat();

            if(chatId is null)
            {
                chat = chats
                    .OrderByDescending(x => x.Messages.OrderByDescending(m => m.CreatedOn).First())
                    .FirstOrDefault(x => x.TeamId == teamId 
                        && x.ChatType == ChatType.Team 
                        && x.Users.Any(u => u.Id == userId));

                if(chat is null)
                {
                    return null;
                }
            }
            else
            {
                chat = chats
                    .FirstOrDefault(x => x.Id == chatId);

                if(chat is null)
                {
                    throw new ArgumentException(InvalidChat);
                }
            }

            var chatModel = this.GetChat(userId, chat);
            chatModel.Name = chat.Name;
            chatModel.IsPersonal = false;

            return chatModel;
        }

        public ChatServiceModel GetPersonalChat(string userId, string secondUserId)
        {
            var chats = this.data.Chats
                    .Include(x => x.Users)
                    .ThenInclude(x => x.Messages);

            var chat = new Chat();

            var friend = new User();

            if (secondUserId is null)
            {
                chat = chats
                    .OrderByDescending(x => x.Messages.OrderByDescending(m => m.CreatedOn).First().CreatedOn)
                    .FirstOrDefault(x => x.Users.Any(u => u.Id == userId)
                        && x.ChatType == ChatType.Personal);

                if (chat is null)
                {
                    return null;
                }

                secondUserId = chat.Users.FirstOrDefault(x => x.Id != userId).Id;
            }
            else
            {
                chat = chats
                    .FirstOrDefault(x => x.Users.Any(u => u.Id == userId)
                        && x.Users.Any(u => u.Id == secondUserId)
                        && x.ChatType == ChatType.Personal);

                if (chat is null)
                {
                    throw new ArgumentException(InvalidChatParticipants);
                }

            }

            friend = chat.Users.FirstOrDefault(x => x.Id != userId);

            var chatModel = this.GetChat(userId, chat);
            chatModel.Name = friend.UserName;
            chatModel.IsPersonal = true;

            return chatModel;
        }

        public IEnumerable<ChatListServiceModel> GetChatList(string teamId, string userId, string selectedChatId = null)
        {
            var chats = this.data.Chats
                .Include(x => x.Users)
                .Where(x => x.TeamId == teamId && x.Users.Any(x => x.Id == userId))
                .Select(x => new ChatListServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsSelected = x.Id == selectedChatId,
                    IsRead = this.IsChatRead(userId, selectedChatId),
                    LastMessageSent = this.GetLastMessage(userId, selectedChatId)
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
                LastReadMessageId = null
            };

            var secondChatUser = new ChatUser
            {
                ChatId = chat.Id,
                UserId = secondUserId,
                LastReadMessageId = null
            };

            this.data.Chats.Add(chat);
            this.data.ChatUsers.Add(chatUser);
            this.data.ChatUsers.Add(secondChatUser);

            this.data.SaveChanges();

            return chat.Id;
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

            var chatUser = this.data.ChatUsers.FirstOrDefault(x => x.UserId == userId && x.ChatId == chat.Id);
            chatUser.LastReadMessageId = message.Id;

            this.data.Messages.Add(message);

            this.data.SaveChanges();

            return message.Id;
        }

        public ChatMessageServiceModel GetLastMessage(string userId, string chatId)
        {
            var messages = this.data.Messages
                   .Include(x => x.User)
                   .Where(x => x.ChatId == chatId)
                   .ToList();

            return messages
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new ChatMessageServiceModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    DateTime = x.CreatedOn.ToString("MM/dd HH:mm"),
                    IsOwn = x.UserId == userId,
                    Sender = new ChatMemberServiceModel
                    {
                        ImagePath = x.User.ImagePath,
                        Username = x.User.UserName
                    }
                }).FirstOrDefault();
        }

        private ChatServiceModel GetChat(string userId, Chat chat)
        {
            var chatUser = this.data.ChatUsers.FirstOrDefault(x => x.UserId == userId && x.ChatId == chat.Id);

            var chatModel = new ChatServiceModel
            {
                Id = chat.Id,
                TeamId = chat.TeamId,
                LastReadMessageId = chatUser.LastReadMessageId,
                Messages = chat.Messages
                    .OrderBy(x => x.CreatedOn)
                    .Select(x => new ChatMessageServiceModel
                    {
                        Id = x.Id,
                        Content = x.Content,
                        DateTime = x.CreatedOn.ToString("MM/dd HH:mm"),
                        IsOwn = x.UserId == userId,
                        Sender = new ChatMemberServiceModel
                        {
                            ImagePath = x.User.ImagePath,
                            Username = x.User.UserName
                        }
                    }).ToList(),
                Members = chat.Users
                    .Select(x => new ChatMemberServiceModel
                    {
                        ImagePath = x.ImagePath,
                        Username = x.UserName
                    }).ToList(),
            };

            chatUser.LastReadMessageId = this.GetLastMessage(userId, chat.Id)?.Id;
            this.data.SaveChanges();

            return chatModel;
        }

        public bool IsChatRead(string userId, string chatId)
        {
            var chatUser = this.data.ChatUsers.FirstOrDefault(x => x.UserId == userId && x.ChatId == chatId);

            var lastMessageId = this.GetLastMessage(userId, chatId)?.Id;

            return chatUser.LastReadMessageId == lastMessageId;
        }
    }
}
