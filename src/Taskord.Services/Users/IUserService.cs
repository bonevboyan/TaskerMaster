namespace Taskord.Services.Users
{
    using Taskord.Services.Users.Models;

    public interface IUserService
    {
        IEnumerable<UserListServiceModel> GetUserList(string teamId);
    }
}
