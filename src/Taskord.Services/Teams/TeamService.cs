namespace Taskord.Services.Teams
{
    using Taskord.Data;
    using Taskord.Data.Models;
    using Taskord.Services.Teams.Models;

    public class TeamService : ITeamService
    {
        private readonly TaskordDbContext data;

        public TeamService(TaskordDbContext data)
        {
            this.data = data;
        }

        public string Create(string name, string description, string imageUrl)
        {
            Schedule schedule = new Schedule();

            schedule.Buckets.Add(new Bucket
            {
                Name = "To Do",
            });
            schedule.Buckets.Add(new Bucket
            {
                Name = "Done",
            });

            data.Schedules.Add(schedule);

            Team team = new Team
            {
                Name = name,
                Description = description,
                ImagePath = imageUrl,
                ScheduleId = schedule.Id,
            };

            data.Teams.Add(team);
            data.SaveChanges();

            return team.Id;
        }

        public IEnumerable<TeamListServiceModel> GetTeamList(string userId)
        {
            var teamList = data.Teams
                .Select(t => new TeamListServiceModel
                {
                    ImagePath = t.ImagePath,
                    Name = t.Name
                })
                .ToList();

            return teamList;
        }
    }
}
