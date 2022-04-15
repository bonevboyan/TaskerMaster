namespace Taskord.Web.Models.Api
{
    using System.ComponentModel.DataAnnotations;

    public class ManageChatUserPostModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string ChatId { get; set; }

        [Required]
        public string TeamId { get; set; }

        [Required]
        public bool IsAdded { get; set; }
    }
}
