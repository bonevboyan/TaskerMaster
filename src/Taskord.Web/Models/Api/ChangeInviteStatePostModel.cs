namespace Taskord.Web.Models.Api
{
    using System.ComponentModel.DataAnnotations;

    public class RespondToInvitePostModel
    {
        [Required]
        public bool IsAccepted { get; set; }

        [Required]
        public string TeamInviteId { get; set; }
    }
}
