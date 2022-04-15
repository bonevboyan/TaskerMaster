namespace Taskord.Services.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using Taskord.Data;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Chats;
    using Taskord.Services.Posts;
    using Taskord.Services.Users.Models;
    using Microsoft.EntityFrameworkCore;

    using static Taskord.Common.ErrorMessages.User;
    using Taskord.Services.Teams;
    using Taskord.Services.Relationships;

    public class UserService : IUserService
    {
        private readonly TaskordDbContext data;
        private readonly IChatService chatService;
        private readonly ITeamService teamService;
        private readonly IRelationshipService relationshipService;

        public UserService(TaskordDbContext data, IChatService chatService, ITeamService teamService, IRelationshipService relationshipService)
        {
            this.data = data;
            this.chatService = chatService;
            this.teamService = teamService;
            this.relationshipService = relationshipService;
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

        public IEnumerable<FriendsChatListServiceModel> GetFriendsChatList(string userId, string chatId)
        {
            var friends = this.data.Relationships
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
            var friends = this.data.Relationships
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

        public UserQueryServiceModel GetQueryUsers(string userId, string searchTerm = null, int currentPage = 1, int usersPerPage = int.MaxValue)
        {
            var usersQuery = this.data.Users.Where(x => x.Id != userId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                usersQuery = usersQuery
                    .Where(x => x.UserName.Contains(searchTerm));
            }

            var totalUsers = usersQuery.Count();

            var friendships = this.data.Relationships.ToList();

            var users = usersQuery
                .ToList()
                .Select(x => new UserListServiceModel
                {
                    Id = x.Id,
                    Name = x.UserName,
                    ImagePath = x.ImagePath,
                    RelationshipState = this.relationshipService.GetRelationship(x.Id, userId)?.State
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

        public UserProfileServiceModel GetUserProfile(string userId)
        {
            var user = this.data.Users
                .Include(x => x.Posts)
                .FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                throw new ArgumentException(InvalidUserId);
            }

            var profile = new UserProfileServiceModel
            {
                FriendCount = this.data.Relationships
                    .Count(x => (x.SenderId == userId || x.ReceiverId == userId) && x.State == RelationshipState.Accepted),
                TeamCount = this.data.UserTeams
                    .Count(x => (x.UserId == userId && x.State == RelationshipState.Accepted)),
                Email = user.Email,
                Username = user.UserName,
                ImagePath = user.ImagePath,
                Id = userId,
                PostsCount = user.Posts.Count
            };

            return profile;
        }
    }
}
