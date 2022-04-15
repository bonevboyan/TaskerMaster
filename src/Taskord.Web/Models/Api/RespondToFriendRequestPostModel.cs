namespace Taskord.Web.Models.Api
{
    using System.ComponentModel.DataAnnotations;

    public class RespondToFriendRequestPostModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public bool IsAccepted { get; set; }
    }
}
