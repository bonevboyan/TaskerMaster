namespace Taskord.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Taskord.Data.Common;

    public class Schedule : BaseModel
    {
        public Schedule()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Buckets = new HashSet<Bucket>();
        }

        public ICollection<Bucket> Buckets { get; set; }
    }
}
