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
                    IsFriend = x.Friends.Any(f => f.Id == userId)
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
                    Name = x.Sender.UserName
                })
                .ToList();

            return pendingRequests;
        }

        public IEnumerable<UserListServiceModel> GetUserFriendsList(string userId)
        {
            var friends = this.data.Users
                .FirstOrDefault(x => x.Id == userId)
                .Friends
                .Select(x => new UserListServiceModel 
                {
                    Id = x.Id,
                    Name = x.UserName, 
                    ImagePath = x.ImagePath, 
                    IsFriend = true 
                })
                .ToList();

            return friends;
        }

        public IEnumerable<UserListServiceModel> GetUserSentFriendRequests(string userId)
        {
            var pendingRequests = this.data.Friendships
                .Include(x => x.Receiver)
                .Where(x => x.SenderId == userId && x.State == RelationshipState.Pending)
                .Select(x => new UserListServiceModel
                {
                    ImagePath = x.Receiver.ImagePath,
                    Name = x.Receiver.UserName
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

            var users = usersQuery
                .Select(x => new UserListServiceModel
                {
                    Id = x.Id,
                    Name = x.UserName,
                    ImagePath = x.ImagePath,
                    IsFriend = x.Friends.Any(f => f.Id == userId)
                })
                .Skip((currentPage - 1) * usersPerPage)
                .Take(usersPerPage)
                .ToList();

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
            if(senderId == receiverId)
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
                this.chatService.CreatePersonalChat(senderId, receiverId);
            }

            this.data.SaveChanges();
        }
    }
}
