namespace Taskord.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Taskord.Data.Models.Enums;
    using Taskord.Data.Common;

    using static Taskord.Common.DataConstants.Tag;

    public class Tag : BaseModel
    {
        public Tag()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Cards = new HashSet<Card>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public Color Color { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
