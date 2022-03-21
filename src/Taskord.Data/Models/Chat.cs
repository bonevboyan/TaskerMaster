namespace Taskord.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Taskord.Data.Common;

    using static Taskord.Common.DataConstants.Chat;

    public class Chat : BaseModel
    {
        public Chat()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Messages = new HashSet<Message>();
            this.Users = new HashSet<ApplicationUser>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

        public string TeamId { get; set; }

        public Team Team { get; set; }
    }
}
