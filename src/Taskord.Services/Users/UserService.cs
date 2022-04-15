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
    using Taskord.Services.Teams;

    public class UserService : IUserService
    {
        private readonly TaskordDbContext data;
        private readonly IChatService chatService;
        private readonly ITeamService teamService;

        public UserService(TaskordDbContext data, IChatService chatService, ITeamService teamService)
        {
            this.data = data;
            this.chatService = chatService;
            this.teamService = teamService;
        }


        public IEnumerable<UserTeamChatManageListServiceModel> GetTeamChatMembersList(string userId, string teamId, string chatId)
        {
            var teamMembers = this.data.UserTeams
                .Include(x => x.User)
                .Include(x => x.Team)
                .Where(x => x.TeamId == teamId)
                .ToList();

            if (!teamMembers.Any())
            {
                throw new ArgumentException(InvalidTeamId);
            }

            var members = teamMembers
                .Where(x => x.UserId != userId)
                .Select(x => new UserTeamChatManageListServiceModel
                {
                    Id = x.UserId,
                    Name = x.User.UserName,
                    ImagePath = x.User.ImagePath,
                    IsInChat = this.chatService.IsUserInChat(x.UserId, chatId)
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

        public IEnumerable<FriendsChatListServiceModel> GetFriendsChatList(string userId, string chatId)
        {
            var friends = this.data.Friendships
                .Include(x => x.Receiver)
                .Include(x => x.Sender)
                .Include(x => x.Chat)
                .ThenInclude(x => x.Messages)
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
                .Select(x => new FriendsChatListServiceModel
                {
                    ImagePath = x.User.ImagePath,
                    Name = x.User.UserName,
                    Id = x.User.Id,
                    LastMessageSent = this.chatService.GetLastMessage(userId, x.ChatId),
                    IsRead = this.chatService.IsChatRead(userId, x.ChatId),
                    IsSelected = x.ChatId == chatId
                }).ToList();

            return allFriends;
        }

        public IEnumerable<UserInviteListServiceModel> GetInviteFriendsList(string userId, string teamId)
        {
            var friends = this.data.Friendships
                .Include(x => x.Receiver)
                .Include(x => x.Sender)
                .ToList();

            var sentFriends = friends
                .Where(x => x.SenderId == userId && x.State == RelationshipState.Accepted)
                .Select(x => x.Receiver)
                .ToList();

            var receivedFriends = friends
                .Where(x => x.ReceiverId == userId && x.State == RelationshipState.Accepted)
                .Select(x => x.Sender)
                .ToList();

            sentFriends.AddRange(receivedFriends);

            var allFriends = sentFriends
                .Select(x => new UserInviteListServiceModel
                {
                    ImagePath = x.ImagePath,
                    Name = x.UserName,
                    Id = x.Id,
                    State = this.teamService.IsUserInvited(x.Id, teamId) ?? RelationshipState.Withdrawn
                })
                .Where(x => x.State != RelationshipState.Accepted)
                .ToList();

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

            var relationship = this.data.Friendships
                .FirstOrDefault(x => x.SenderId == senderId && x.ReceiverId == receiverId 
                || x.ReceiverId == senderId && x.SenderId == receiverId);

            if (relationship?.State == RelationshipState.Accepted)
            {
                throw new ArgumentException(FriendshipAlreadyExists);
            }

            if(relationship == null)
            {

                relationship = new Friendship
                {
                    SenderId = senderId,
                    ReceiverId = receiverId
                };

                this.data.Friendships.Add(relationship);
            }
            else
            {
                relationship.State = RelationshipState.Pending;
            }

            this.data.SaveChanges();

            return relationship.Id;
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

        public RelationshipState? GetRelationshipState(string userId, string secondUserId)
        {
            var state = this.data.Friendships
                .FirstOrDefault(x => (x.SenderId == userId && x.ReceiverId == secondUserId)
                    || (x.ReceiverId == userId && x.SenderId == secondUserId))?
                .State;

            return state;
        }

        public IEnumerable<UserManageRolesServiceModel> GetRoleManageTeamMembersList(string userId, string teamId)
        {
            var teamMembers = this.data.UserTeams
                .Include(x => x.User)
                .Include(x => x.Team)
                .Where(x => x.TeamId == teamId)
                .ToList();

            if (!teamMembers.Any())
            {
                throw new ArgumentException(InvalidTeamId);
            }

            var members = teamMembers
                .Where(x => x.UserId != userId)
                .Select(x => new UserManageRolesServiceModel
                {
                    Id = x.UserId,
                    Name = x.User.UserName,
                    ImagePath = x.User.ImagePath,
                    Role = x.Role
                })
                .ToList();

            return members;
        }
    }
}
