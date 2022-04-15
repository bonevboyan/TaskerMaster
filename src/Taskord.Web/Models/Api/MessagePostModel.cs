namespace Taskord.Web.Models.Api
{
    using System.ComponentModel.DataAnnotations;

    using static Taskord.Common.DataConstants.Message;

    public class MessagePostModel
    {
        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; }

        [Required]
        public string ChatId { get; set; }
    }
}
