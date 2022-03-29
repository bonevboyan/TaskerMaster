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

        public DbSet<Friendship> Friendships { get; set; }

        public DbSet<ChatUser> ChatUsers { get; set; }


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

            builder
                .Entity<UserTeam>()
                .HasKey(x => new { x.UserId, x.TeamId });

            builder
                .Entity<ChatUser>()
                .HasKey(x => new { x.ChatId, x.UserId });

            builder
                .Entity<Friendship>()
                .HasOne(x => x.Receiver)
                .WithMany(x => x.Friendships)
                .HasForeignKey(x => x.ReceiverId);

            builder
                .Entity<User>()
                .HasMany(x => x.Chats)
                .WithMany(x => x.Users);

            base.OnModelCreating(builder);
        }

    }
}