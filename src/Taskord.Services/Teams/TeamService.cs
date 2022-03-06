namespace Taskord.Services.Teams
{
    using Taskord.Data;
    using Taskord.Data.Models;

    public class TeamService : ITeamService
    {
        private readonly TaskordDbContext data;

        public TeamService(TaskordDbContext data)
        {
            this.data = data;
        }

        public string Create(string name, string description, string imageUrl)
        {
            Team team = new Team
            {
                Name = name,
                Description = description,
                ImagePath = imageUrl
            };

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

            team.ScheduleId = schedule.Id;

            data.Teams.Add(team);
            data.SaveChanges();

            return team.Id;
        }
    }
}
