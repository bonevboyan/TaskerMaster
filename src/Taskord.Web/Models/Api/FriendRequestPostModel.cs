namespace Taskord.Web.Models.Api
{
    using System.ComponentModel.DataAnnotations;

    public class FriendRequestPostModel
    {
        [Required]
        public string UserId { get; set; }
    }
}
