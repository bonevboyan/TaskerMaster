namespace Taskord.Services.Chats
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Taskord.Data;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Chats.Models;
    using Taskord.Services.Teams;
    using Taskord.Services.Users.Models;
    using static Taskord.Common.ErrorMessages.Chat;

    public class ChatService : IChatService
    {
        private readonly TaskordDbContext data;
        private readonly ITeamService teamService;

        public ChatService(TaskordDbContext data, ITeamService teamService)
        {
            this.data = data;
            this.teamService = teamService;
        }

        public string CreateTeamChat(string teamId, string name)
        {
            var team = this.data.Teams.FirstOrDefault(x => x.Id == teamId);

            if (team == null)
            {
                throw new ArgumentException(InvalidTeam);
            }

            var users = this.data.UserTeams
                .Where(x => x.Role == TeamRole.Admin && x.TeamId == teamId)
                .Select(x => x.User)
                .ToList();

            var chat = new Chat
            {
                Name = name,
                Users = users,
                ChatType = ChatType.Team
            };

            foreach (var user in users)
            {
                chat.ChatUsers.Add(new ChatUser
                {
                    UserId = user.Id,
                    ChatId = chat.Id,
                    LastReadMessageId = null
                });
            }

            team.Chats.Add(chat);
            this.data.SaveChanges();

            return chat.Id;
        }

        public ChatServiceModel GetTeamChat(string userId, string teamId, string chatId)
        {

            var chats = this.data.Chats
                    .Include(x => x.Messages)
                    .ThenInclude(x => x.User)
                    .Include(x => x.Users)
                    .Include(x => x.ChatUsers);

            var chat = new Chat();

            if (chatId is null)
            {
                chat = chats
                    .OrderByDescending(x => x.Messages.Any() ? x.Messages.OrderByDescending(x => x.CreatedOn).FirstOrDefault().CreatedOn : x.CreatedOn)
                    .FirstOrDefault(x => x.TeamId == teamId
                        && (x.ChatType == ChatType.Team || x.ChatType == ChatType.General)
                        && x.Users.Any(u => u.Id == userId));

                if (chat is null)
                {
                    return null;
                }
            }
            else
            {
                if (chatId.Contains("@"))
                {
                    return null;
                }

                chat = chats
                    .FirstOrDefault(x => x.Id == chatId);

                if (chat is null || chat.TeamId != teamId)
                {
                    throw new ArgumentException(InvalidChat);
                }
            }

            var chatModel = this.GetChat(userId, chat);
            chatModel.Name = chat.Name;
            chatModel.ChatType = chat.ChatType;
            chatModel.IsAdmin = this.teamService.IsAdmin(userId, teamId);

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
                    .OrderByDescending(x => x.Messages.Any() ? x.Messages.OrderByDescending(x => x.CreatedOn).FirstOrDefault().CreatedOn : x.CreatedOn)
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
            chatModel.ChatType = chat.ChatType;

            return chatModel;
        }

        public IEnumerable<ChatListServiceModel> GetChatList(string teamId, string userId, string selectedChatId = null)
        {
            var chats = this.data.Chats
                .Include(x => x.Users)
                .Include(x => x.Messages)
                .ToList();

            var chatList = chats
                .OrderByDescending(x => x.Messages.Any() ? x.Messages.OrderByDescending(x => x.CreatedOn).FirstOrDefault().CreatedOn : x.CreatedOn)
                .Where(x => x.TeamId == teamId && x.Users.Any(x => x.Id == userId))
                .Select(x => new ChatListServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsSelected = x.Id == selectedChatId,
                    IsRead = this.IsChatRead(userId, x.Id),
                    LastMessageSent = this.GetLastMessage(userId, x.Id)
                })
                .ToList();

            return chatList;
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

            if (chat is null)
            {
                throw new ArgumentException(InvalidChat);
            }

            if (!chat.Users.Any(x => x.Id == userId))
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

            var message = messages
                .OrderByDescending(x => x.CreatedOn)
                .FirstOrDefault();

            return  message is null ? null : new ChatMessageServiceModel
            {
                Id = message.Id,
                Content = message.Content,
                DateTime = message.CreatedOn.ToString("MM/dd HH:mm"),
                IsOwn = message.UserId == userId,
                Sender = new UserListServiceModel
                {
                    ImagePath = message.User.ImagePath,
                    Name = message.User.UserName
                }
            };
        }

        private ChatServiceModel GetChat(string userId, Chat chat)
        {
            var chatUser = this.data.ChatUsers.FirstOrDefault(x => x.UserId == userId && x.ChatId == chat.Id);

            if (chatUser is null)
            {
                throw new ArgumentException(UserNotInChat);
            }

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
                        Sender = new UserListServiceModel
                        {
                            ImagePath = x.User.ImagePath,
                            Name = x.User.UserName,
                            Id = x.User.Id
                        }
                    }).ToList(),
                Members = chat.Users
                    .Select(x => new UserListServiceModel
                    {
                        ImagePath = x.ImagePath,
                        Name = x.UserName,
                        Id = x.Id
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

        public bool IsUserInChat(string userId, string chatId)
        {
            return this.data.ChatUsers.Any(x => x.UserId == userId && x.ChatId == chatId);
        }
    }
}
