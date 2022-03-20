namespace Taskord.Services.Teams
{
    using Taskord.Services.Teams.Models;

    public interface ITeamService
    {
        string Create(string name, string description, string imageUrl, IEnumerable<string> userIds);

        IEnumerable<TeamListServiceModel> GetTeamList(string userId);
    }
}
