namespace TaskerMaster.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TaskerMaster.Data.Common.Models;

    using static TaskerMaster.Common.DataConstants.Bucket;

    public class Bucket : BaseDeletableModel<string>
    {
        public Bucket()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Cards = new HashSet<Card>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
