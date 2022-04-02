namespace Taskord.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Taskord.Data.Common;
    using Taskord.Data.Models.Enums;

    public class UserTeam : BaseModel
    {
        public UserTeam()
            : base()
        {
            this.State = RelationshipState.Pending;
        }

        public RelationshipState State { get; set; }

        public TeamRole Role { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string InviterId { get; set; }

        public virtual User Inviter { get; set; }

        public string TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
