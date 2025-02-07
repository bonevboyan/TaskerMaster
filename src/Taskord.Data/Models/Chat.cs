﻿namespace Taskord.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Taskord.Data.Common;
    using Taskord.Data.Models.Enums;
    using static Taskord.Common.DataConstants.Chat;

    public class Chat : BaseModel
    {
        public Chat()
            : base()
        {
            this.Messages = new HashSet<Message>();
            this.Users = new HashSet<User>();
            this.ChatUsers = new HashSet<ChatUser>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<ChatUser> ChatUsers { get; set; }

        public ICollection<User> Users { get; set; }

        public string TeamId { get; set; }

        public Team Team { get; set; }

        public ChatType ChatType { get; set; }
    }
}
