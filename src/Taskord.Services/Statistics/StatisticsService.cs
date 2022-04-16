namespace Taskord.Services.Statistics
{
    using Taskord.Data;
    using Taskord.Services.Statistics.Models;

    public class StatisticsService : IStatisticsService
    {
        private readonly TaskordDbContext data;

        public StatisticsService(TaskordDbContext data)
        {
            this.data = data;
        }

        public StatisticsServiceModel Total()
        {
            return new StatisticsServiceModel
            {
                TotalPosts = this.data.Posts.Count(),
                TotalUsers = this.data.Users.Count(),
                TotalTeams = this.data.Teams.Count()
            };
        }
    }
}
