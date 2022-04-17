namespace Taskord.Test.Services
{
    using Taskord.Data.Models;
    using Xunit;

    public class StatisticsServiceTest : BaseServiceTests
    {
        public StatisticsServiceTest()
            : base()
        {
            this.data.Posts.AddRange(new Post { }, new Post { }, new Post { });
            this.data.Users.AddRange(new User { }, new User { });
            this.data.Teams.AddRange(new Team { });
            this.data.SaveChanges();
        }

        [Fact]
        public void TotalShouldReturnCorrectValues()
        {
            var statistics = this.statisticsService.Total();

            Assert.Equal(3, statistics.TotalPosts);
            Assert.Equal(2, statistics.TotalUsers);
            Assert.Equal(1, statistics.TotalTeams);
        }
    }
}
