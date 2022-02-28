namespace TaskerMaster.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TaskerMaster.Data.Common.Models;
    using TaskerMaster.Data.Models.Enums;

    using static TaskerMaster.Common.DataConstants.Task;

    public class Card : BaseDeletableModel<string>
    {
        public Card()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Tags = new HashSet<Tag>();
            this.Users = new HashSet<ApplicationUser>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public TaskCompletion State { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
