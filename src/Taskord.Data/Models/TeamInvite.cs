namespace Taskord.Data.Models
{
    using Taskord.Data.Common;
    using Taskord.Data.Models.Enums;

    public class TeamInvite : BaseModel
    {
        public TeamInvite()
            :base()
        {
            this.State = RelationshipState.Pending;
        }

        public RelationshipState State { get; set; }

        public string TeamId { get; set; }

        public Team Team { get; set; }

        public string SenderId { get; set; }

        public User Sender { get; set; }

        public string ReceiverId { get; set; }

        public User Receiver { get; set; }
    }
}
