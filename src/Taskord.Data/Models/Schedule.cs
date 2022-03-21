namespace Taskord.Data.Models
{
    using System.Collections.Generic;
    using Taskord.Data.Common;

    public class Schedule : BaseModel
    {
        public Schedule()
            : base()
        {
            this.Buckets = new HashSet<Bucket>();
        }

        public string TeamId { get; set; }

        public Team Team { get; set; }

        public ICollection<Bucket> Buckets { get; set; }
    }
}
