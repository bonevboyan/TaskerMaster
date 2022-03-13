namespace Taskord.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Taskord.Data.Common;

    using static Taskord.Common.DataConstants.Team;

    public class Team : BaseModel
    {
        public Team()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Members = new HashSet<ApplicationUser>();
            this.Chats = new HashSet<Chat>();
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

        public ICollection<ApplicationUser> Members { get; set; }

        public ICollection<AdminTeam> AdminTeams { get; set; }

        public ICollection<Chat> Chats { get; set; }
    }
}
