namespace Taskord.Services.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using Taskord.Data;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Chats;
    using Taskord.Services.Users.Models;
    using Microsoft.EntityFrameworkCore;

    using static Taskord.Common.ErrorMessages.User;
    using Taskord.Services.Chats.Models;

    public class UserService : IUserService
    {
        private readonly TaskordDbContext data;
        private readonly IChatService chatService;

        public UserService(TaskordDbContext data, IChatService chatService)
        {
            this.data = data;
            this.chatService = chatService;
        }


        public IEnumerable<UserListServiceModel> GetTeamMembersList(string teamId, string userId)
        {
            var members = this.data.Users
                .Where(x => x.UserTeams.Any(t => t.Team.Id == teamId))
                .Select(x => new UserListServiceModel
                {
                    Id = x.Id,
                    Name = x.UserName,
                    ImagePath = x.ImagePath,
                    //IsFriend = x.Friendships.Any(f => (f.SenderId == userId || f.ReceiverId == userId) && f.State == RelationshipState.Accepted)
                })
                .ToList();

            return members;
        }

        public IEnumerable<UserListServiceModel> GetUserReceivedFriendRequests(string userId)
        {
            var pendingRequests = this.data.Friendships
                .Where(x => x.ReceiverId == userId && x.State == RelationshipState.Pending)
                .Select(x => new UserListServiceModel
                {
                    ImagePath = x.Sender.ImagePath,
                    Name = x.Sender.UserName,
                    Id = x.Sender.Id
                })
                .ToList();

            return pendingRequests;
        }

        public IEnumerable<UserFriendListServiceModel> GetUserFriendsList(string userId, string chatId)
        {
            var friends = this.data.Friendships
                .Include(x => x.Receiver)
                .Include(x => x.Sender)
                .Include(x => x.Chat)
                .ThenInclude(x => x.Messages)
                .Where(x => x.State == RelationshipState.Accepted)
                .ToList();

            var sentFriends = friends
                .Where(x => x.SenderId == userId && x.State == RelationshipState.Accepted)
                .Select(x => new LastMessageServiceModel 
                { 
                    User = x.Receiver, 
                    ChatId = x.ChatId,
                    CreatedOn = x.Chat.Messages.Any() ? x.Chat.Messages.OrderByDescending(x => x.CreatedOn).FirstOrDefault().CreatedOn : x.Chat.CreatedOn
                })
                .ToList();

            var receivedFriends = friends
                .Where(x => x.ReceiverId == userId && x.State == RelationshipState.Accepted)
                .Select(x => new LastMessageServiceModel
                {
                    User = x.Sender,
                    ChatId = x.ChatId,
                    CreatedOn = x.Chat.Messages.Any() ? x.Chat.Messages.OrderByDescending(x => x.CreatedOn).FirstOrDefault().CreatedOn : x.Chat.CreatedOn
                })
                .ToList();

            sentFriends.AddRange(receivedFriends);

            var allFriends = sentFriends
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new UserFriendListServiceModel
                {
                    ImagePath = x.User.ImagePath,
                    Name = x.User.UserName,
                    Id = x.User.Id,
                    LastMessageSent = this.chatService.GetLastMessage(userId, x.User.Id),
                    IsRead = this.IsChatRead(userId, x.User.Id),
                    IsSelected = x.ChatId == chatId
                }).ToList();

            return allFriends;
        }

        public IEnumerable<UserListServiceModel> GetUserSentFriendRequests(string userId)
        {
            var pendingRequests = this.data.Friendships
                .Include(x => x.Receiver)
                .Where(x => x.SenderId == userId && x.State == RelationshipState.Pending)
                .Select(x => new UserListServiceModel
                {
                    ImagePath = x.Receiver.ImagePath,
                    Name = x.Receiver.UserName,
                    Id = x.Receiver.Id
                })
                .ToList();

            return pendingRequests;
        }

        public UserQueryServiceModel GetQueryUsers(string userId, string searchTerm = null, int currentPage = 1, int usersPerPage = int.MaxValue)
        {
            var usersQuery = this.data.Users.Where(x => x.Id != userId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                usersQuery = usersQuery
                    .Where(x => x.UserName.Contains(searchTerm));
            }

            var totalUsers = usersQuery.Count();

            var friendships = this.data.Friendships.ToList();

            var users = usersQuery
                .ToList()
                .Select(x => new UserListServiceModel
                {
                    Id = x.Id,
                    Name = x.UserName,
                    ImagePath = x.ImagePath,
                    RelationshipState = this.GetRelationshipState(x.Id, userId)
                })
                .Skip((currentPage - 1) * usersPerPage)
                .Take(usersPerPage);

            return new UserQueryServiceModel
            {
                TotalUsers = totalUsers,
                CurrentPage = currentPage,
                UsersPerPage = usersPerPage,
                Users = users
            };
        }

        public string SendFriendRequest(string senderId, string receiverId)
        {
            if (senderId == receiverId)
            {
                throw new ArgumentException(InvalidFriendRequestParameters);
            }

            var friendRequest = new Friendship
            {
                SenderId = senderId,
                ReceiverId = receiverId
            };

            this.data.Friendships.Add(friendRequest);
            this.data.SaveChanges();

            return friendRequest.Id;
        }

        public void ChangeRelationshipState(string senderId, string receiverId, RelationshipState state)
        {
            var request = this.data.Friendships
                .FirstOrDefault(x => x.SenderId == senderId && x.ReceiverId == receiverId);

            if (request is null)
            {
                throw new ArgumentException(InvalidFriendRequest);
            }

            request.State = state;

            if (state == RelationshipState.Accepted)
            {
                var chatId = this.chatService.CreatePersonalChat(senderId, receiverId);

                request.ChatId = chatId;
            }

            this.data.SaveChanges();
        }

        private RelationshipState? GetRelationshipState(string userId, string secondUserId)
        {
            var state = this.data.Friendships
                .FirstOrDefault(x => (x.SenderId == userId && x.ReceiverId == secondUserId)
                    || (x.ReceiverId == userId && x.SenderId == secondUserId))?
                .State;

            return state;
        }



        private bool IsChatRead(string userId, string secondUserId)
        {
            var chat = this.data.Chats
                .FirstOrDefault(x => x.Users.Any(u => u.Id == userId)
                    && x.Users.Any(u => u.Id == secondUserId)
                    && x.ChatType == ChatType.Personal);

            var chatUsers = this.data.ChatUsers.ToList();

            var chatUser = chatUsers.FirstOrDefault(x => x.UserId == userId && x.ChatId == chat.Id);

            var lastMessageId = this.chatService.GetLastMessage(userId, secondUserId)?.Id;

            return chatUser.LastReadMessageId == lastMessageId;
        }
    }
}
