namespace Taskord.Web.Models.Api
{
    public class RespondToFriendRequestPostModel
    {
        public string UserId { get; set; }

        public bool IsAccepted { get; set; }
    }
}
