namespace Taskord.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Taskord.Data.Models;

    public class TaskordDbContext : IdentityDbContext<User>
    {
        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Relationship> Relationships { get; set; }

        public DbSet<ChatUser> ChatUsers { get; set; }

        public DbSet<UserTeam> UserTeams { get; set; }

        public DbSet<Post> Posts { get; set; }

        public TaskordDbContext(DbContextOptions<TaskordDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<ChatUser>()
                .HasKey(x => new { x.ChatId, x.UserId });

            builder
                .Entity<Relationship>()
                .HasOne(x => x.Receiver)
                .WithMany(x => x.Relationships)
                .HasForeignKey(x => x.ReceiverId); 
            
            builder
                .Entity<UserTeam>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserTeams)
                .HasForeignKey(x => x.UserId);

            builder
                .Entity<User>()
                .HasMany(x => x.Chats)
                .WithMany(x => x.Users);

            base.OnModelCreating(builder);
        }

    }
}