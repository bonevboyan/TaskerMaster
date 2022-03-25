namespace Taskord.Services.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using Taskord.Data;
    using Taskord.Data.Models;
    using Taskord.Data.Models.Enums;
    using Taskord.Services.Chats;
    using Taskord.Services.Users.Models;

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
            var pendingRequests = this.data.FriendRequests
                .Where(x => x.ReceiverId == userId)
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
            var pendingRequests = this.data.FriendRequests
                .Where(x => x.SenderId == userId)
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
            var usersQuery = this.data.Users.AsQueryable();

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
            var friendRequest = new Friendship
            {
                SenderId = senderId,
                ReceiverId = receiverId
            };

            this.data.FriendRequests.Add(friendRequest);
            this.data.SaveChanges();

            return friendRequest.Id;
        }

        public void RespondToFriendRequest(string senderId, string receiverId, bool isAccepted)
        {
            var request = this.data.FriendRequests
                .FirstOrDefault(x => x.SenderId == senderId && x.ReceiverId == receiverId);

            if (request is null)
            {
                throw new ArgumentException(InvalidFriendRequest);
            }

            if (isAccepted)
            {
                request.State = FriendRequestState.Accepted;
                this.chatService.CreatePersonalChat(senderId, receiverId);
            }
            else
            {
                request.State = FriendRequestState.Declined;
            }

            this.data.SaveChanges();
        }
    }
}
