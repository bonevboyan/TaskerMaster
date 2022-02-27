namespace TaskerMaster.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TaskerMaster.Data.Common.Models;
    using TaskerMaster.Data.Models.Enums;

    using static TaskerMaster.Common.DataConstants.Message;

    public class Message : BaseDeletableModel<string>
    {
        public Message()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public MessageType MessageType { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
