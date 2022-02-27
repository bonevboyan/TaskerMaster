namespace TaskerMaster.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TaskerMaster.Data.Common.Models;

    using static TaskerMaster.Common.DataConstants.Discussion;

    public class Discussion : BaseDeletableModel<string>
    {
        public Discussion()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Messages = new HashSet<Message>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<Message> Messages { get; set; }

        [Required]
        [ForeignKey(nameof(Team))]
        public string TeamId { get; set; }

        public Team Team { get; set; }
    }
}
