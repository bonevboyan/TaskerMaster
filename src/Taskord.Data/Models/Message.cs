namespace Taskord.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Taskord.Data.Models.Enums;
    using Taskord.Data.Common;

    using static Taskord.Common.DataConstants.Message;

    public class Message : BaseModel
    {
        public Message()
            : base()
        {
        }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        public string ImagePath { get; set; }


        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public string ChatId { get; set; }

        public Chat Chat { get; set; }
    }
}
