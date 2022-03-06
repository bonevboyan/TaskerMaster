namespace Taskord.Services.Teams
{
    public interface ITeamService
    {
        string Create(string name, string description, string imageUrl);
    }
}
