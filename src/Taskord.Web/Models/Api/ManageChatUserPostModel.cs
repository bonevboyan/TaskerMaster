namespace Taskord.Web.Models.Api
{
    public class ManageChatUserPostModel
    {
        public string UserId { get; set; }

        public string ChatId { get; set; }

        public string TeamId { get; set; }

        public bool IsAdded { get; set; }
    }
}
