namespace Taskord.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Taskord.Data.Models.Enums;
    using Taskord.Data.Common;

    using static Taskord.Common.DataConstants.Message;

    public class Message : BaseModel
    {
        public Message()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public MessageType MessageType { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
