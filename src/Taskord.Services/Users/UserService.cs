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
                .Where(x => x.Teams.Any(t => t.Id == teamId))
                .Select(x => new UserListServiceModel { Id = x.Id, Name = x.UserName, ImagePath = x.ImagePath })
                .ToList();

            return members;
        }


        public IEnumerable<UserListServiceModel> GetUserFriendsList(string userId)
        {
            var friends = data.Users
                .FirstOrDefault(x => x.UserName == userId)
                .Connections
                .Select(x => new UserListServiceModel { Id = x.Id, Name = x.UserName, ImagePath = x.ImagePath })
                .ToList();

            return friends;
        }

        public IEnumerable<UserListServiceModel> GetUsersBySearchTerm(string searchTerm)
        {
            var users = data.Users
                .Where(x => x.UserName.Contains(searchTerm))
                .Select(x => new UserListServiceModel { Id = x.Id, Name = x.UserName, ImagePath = x.ImagePath })
                .ToList();

            return users;
        }
    }
}
