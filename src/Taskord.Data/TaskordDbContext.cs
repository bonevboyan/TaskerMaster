namespace Taskord.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Taskord.Data.Models;

    public class TaskordDbContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Bucket> Buckets { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<AdminTeam> AdminTeams { get; set; }


        public TaskordDbContext(DbContextOptions<TaskordDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Team>()
                .HasOne<Schedule>()
                .WithOne()
                .HasForeignKey<Team>(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AdminTeam>().HasKey(x => new { x.TeamId, x.ApplicationUserId });


            base.OnModelCreating(builder);
        }

    }
}