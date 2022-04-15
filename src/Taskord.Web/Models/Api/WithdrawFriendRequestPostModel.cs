namespace Taskord.Web.Models.Api
{
    using System.ComponentModel.DataAnnotations;

    public class WithdrawFriendRequestPostModel
    {
        [Required]
        public string UserId { get; set; }
    }
}
