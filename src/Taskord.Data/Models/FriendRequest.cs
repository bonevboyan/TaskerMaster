namespace Taskord.Data.Models
{
    using Taskord.Data.Common;
    using Taskord.Data.Models.Enums;

    public class FriendRequest : BaseModel
    {
        public FriendRequest()
            : base()
        {
        }

        public FriendRequestState State { get; set; }

        public User Sender { get; set; }

        public string SenderId { get; set; }

        public User Receiver { get; set; }

        public string ReceiverId { get; set; }
    }
}
