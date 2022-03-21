namespace Taskord.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Taskord.Data.Models.Enums;

    public class UserTeam
    {
        public TeamRole Role { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey(nameof(Team))]
        public string TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
