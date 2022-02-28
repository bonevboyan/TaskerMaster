namespace TaskerMaster.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TaskerMaster.Data.Common.Models;

    using static TaskerMaster.Common.DataConstants.Team;

    public class Team : BaseDeletableModel<string>
    {
        public Team()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Employees = new HashSet<ApplicationUser>();
            this.Discussions = new HashSet<Discussion>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required
        //[ForeignKey(nameof(Schedule))]
        public string ScheduleId { get; set; }

        public Schedule Schedule { get; set; }

        [Required]
        [ForeignKey(nameof(Workspace))]
        public string WorkspaceId { get; set; }

        public Workspace Workspace { get; set; }

        public ICollection<ApplicationUser> Employees { get; set; }

        public ICollection<Discussion> Discussions { get; set; }
    }
}
