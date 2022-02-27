﻿namespace TaskerMaster.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TaskerMaster.Data.Common.Models;
    using TaskerMaster.Data.Models.Enums;

    using static TaskerMaster.Common.DataConstants.Tag;

    public class Tag : BaseDeletableModel<string>
    {
        public Tag()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Tasks = new HashSet<Task>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public Color Color { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
