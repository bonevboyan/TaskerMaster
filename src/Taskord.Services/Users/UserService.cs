namespace Taskord.Services.Users
{
    using System.Collections.Generic;
    using Taskord.Data;
    using Taskord.Services.Users.Models;

    public class UserService : IUserService
    {
        private readonly TaskordDbContext data;

        public UserService(TaskordDbContext data)
        {
            this.data = data;
        }


        public IEnumerable<UserListServiceModel> GetTeamMembersList(string teamId)
        {
            var members = data.Users
                .Where(x => x.UserTeams.Any(t => t.Team.Id == teamId))
                .Select(x => new UserListServiceModel { Id = x.Id, Name = x.UserName, ImagePath = x.ImagePath })
                .ToList();

            return members;
        }

        public IEnumerable<UserListServiceModel> GetUserFriendRequests(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserListServiceModel> GetUserFriendsList(string userId)
        {
            var friends = data.Users
                .FirstOrDefault(x => x.Id == userId)
                .Friends
                .Select(x => new UserListServiceModel { Id = x.Id, Name = x.UserName, ImagePath = x.ImagePath })
                .ToList();

            return friends;
        }

        public IEnumerable<UserListServiceModel> GetUserPendingRequests(string userId)
        {
            throw new NotImplementedException();
        }

        public UserQueryServiceModel GetQueryUsers(string searchTerm = null, int currentPage = 1, int usersPerPage = int.MaxValue)
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
    }
}
