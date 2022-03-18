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


        public IEnumerable<UserListServiceModel> GetUserList(string teamId)
        {
            var names = data.ApplicationUsers
                .Where(x => x.Teams.Any(t => t.Id == teamId))
                .Select(x => new UserListServiceModel { Id = x.Id, Name = x.UserName })
                .ToList();

            return names;
        }
    }
}
