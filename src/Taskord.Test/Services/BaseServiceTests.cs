namespace Taskord.Test.Services
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Taskord.Data;
    using Taskord.Services.Chats;
    using Taskord.Services.Posts;
    using Taskord.Services.Relationships;
    using Taskord.Services.Statistics;
    using Taskord.Services.Teams;
    using Taskord.Services.Users;

    public class BaseServiceTests
    {
        protected readonly TaskordDbContext data;
        protected readonly IChatService chatService;
        protected readonly IPostService postService;
        protected readonly IRelationshipService relationshipService;
        protected readonly IStatisticsService statisticsService;
        protected readonly ITeamService teamService;
        protected readonly IUserService userService;

        public BaseServiceTests()
        {
            this.data = GetDb();
            this.teamService = new TeamService(this.data);
            this.chatService = new ChatService(this.data, this.teamService);
            this.relationshipService = new RelationshipService(this.data, this.chatService);
            this.statisticsService = new StatisticsService(this.data);
            this.postService = new PostService(this.data, this.relationshipService);
            this.userService = new UserService(this.data, this.chatService, this.teamService, this.relationshipService);
        }

        public static TaskordDbContext GetDb()
        {
            var options = new DbContextOptionsBuilder<TaskordDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new TaskordDbContext(options);

            return db;
        }
    }
}
