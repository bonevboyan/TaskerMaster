namespace Taskord.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Taskord.Data.Common;

    using static Taskord.Common.DataConstants.Team;

    public class Team : BaseModel
    {
        public Team()
            : base()
        {
            this.UserTeams = new HashSet<UserTeam>();
            this.Chats = new HashSet<Chat>();
            this.TeamInvites = new HashSet<TeamInvite>();
        }

        [Required]
        public string ImagePath { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ScheduleId { get; set; }

        public Schedule Schedule { get; set; }

        public ICollection<UserTeam> UserTeams { get; set; }

        public ICollection<Chat> Chats { get; set; }

        public ICollection<TeamInvite> TeamInvites { get; set; }
    }
}
