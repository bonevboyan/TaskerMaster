namespace Taskord.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Taskord.Data.Common;
    using Taskord.Data.Models.Enums;

    public class Friendship : BaseModel
    {
        public Friendship()
            : base()
        {
            this.State = RelationshipState.Pending;
        }

        public RelationshipState State { get; set; }

        public User Sender { get; set; }

        [ForeignKey(nameof(Sender))]
        public string SenderId { get; set; }

        public User Receiver { get; set; }

        [ForeignKey(nameof(Receiver))]
        public string ReceiverId { get; set; }

        public Chat Chat { get; set; }

        public string ChatId { get; set; }
    }
}
