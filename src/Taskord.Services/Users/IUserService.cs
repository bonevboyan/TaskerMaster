namespace Taskord.Services.Users
{
    public interface IUserService
    {
        IEnumerable<string> GetUserNames(string teamId);
    }
}
