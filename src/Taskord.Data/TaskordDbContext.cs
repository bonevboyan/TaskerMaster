namespace Taskord.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Taskord.Data.Models;

    public class TaskordDbContext : IdentityDbContext<User>
    {
        public DbSet<Bucket> Buckets { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<FriendRequest> FriendRequests { get; set; }


        public TaskordDbContext(DbContextOptions<TaskordDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Team>()
                .HasOne(x => x.Schedule)
                .WithOne(x => x.Team)
                .HasForeignKey<Schedule>(x => x.TeamId);
                //.HasOne<Schedule>()
                //.WithOne()
                //.HasForeignKey<Team>(d => d.ScheduleId)
                //.OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<UserTeam>()
                .HasKey(x => new { x.UserId, x.TeamId });



            base.OnModelCreating(builder);
        }

    }
}