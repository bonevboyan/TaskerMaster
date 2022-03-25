namespace Taskord.Data.Models
{
    using Taskord.Data.Common;
    using Taskord.Data.Models.Enums;

    public class Friendship : BaseModel
    {
        public Friendship()
            : base()
        {
            this.State = FriendRequestState.Pending;
        }

        public FriendRequestState State { get; set; }

        public User Sender { get; set; }

        public string SenderId { get; set; }

        public User Receiver { get; set; }

        public string ReceiverId { get; set; }

        public Chat Chat { get; set; }

        public string ChatId { get; set; }
    }
}
