namespace TaskerMaster.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TaskerMaster.Data.Common.Models;

    using static TaskerMaster.Common.DataConstants.Workspace;

    public class Schedule : BaseDeletableModel<string>
    {
        public Schedule()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Buckets = new HashSet<Bucket>();
        }

        [Required]
        public string TeamId { get; set; }

        public Team Team { get; set; }

        public ICollection<Bucket> Buckets { get; set; }
    }
}
