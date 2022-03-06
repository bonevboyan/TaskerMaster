namespace Taskord.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Taskord.Data.Common;

    using static Taskord.Common.DataConstants.Bucket;

    public class Bucket : BaseModel
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
